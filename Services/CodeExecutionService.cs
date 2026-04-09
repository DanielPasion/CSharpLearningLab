using System.Diagnostics;
using System.Reflection;
using System.Text;
using Basic.Reference.Assemblies;
using CSharpLearningLab.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpLearningLab.Services;

/// <summary>
/// Executes user-submitted C# snippets inside the browser.
///
/// Why not <c>CSharpScript.RunAsync</c>?
/// In Blazor WebAssembly, every loaded assembly has an empty <c>Assembly.Location</c>
/// because assemblies are fetched as byte arrays, not from disk. <c>CSharpScript</c>
/// internally tries to attach a metadata reference to the hosting assembly via
/// <c>MetadataReference.CreateFromFile(assembly.Location)</c> and blows up with
/// <c>NotSupportedException: Can't create a metadata reference to an assembly without
/// location.</c>
///
/// The low-level <c>CSharpCompilation.Create</c> → <c>Emit</c> → <c>Assembly.Load(byte[])</c>
/// path avoids that baggage entirely. We wrap the user snippet in a regular .cs source
/// file (top-level statements compile to a synthesized <c>Program.Main</c>), compile it
/// against the full .NET 9 reference set that ships inside
/// <c>Basic.Reference.Assemblies.Net90</c>, and invoke the resulting entry point via
/// reflection while redirecting <c>Console.Out</c> so we can return what the snippet printed.
/// </summary>
public sealed class CodeExecutionService
{
    private readonly IReadOnlyList<MetadataReference> _references;
    private readonly CSharpParseOptions _parseOptions;
    private readonly CSharpCompilationOptions _compilationOptions;

    // Prepended to every user snippet so the lessons don't need to spell out common usings.
    private const string UsingPreamble = """
        using System;
        using System.IO;
        using System.Text;
        using System.Linq;
        using System.Collections.Generic;
        using System.Threading;
        using System.Threading.Tasks;

        """;

    public CodeExecutionService()
    {
        _references = Net90.References.All.ToList();

        _parseOptions = CSharpParseOptions.Default
            .WithLanguageVersion(LanguageVersion.Latest)
            .WithKind(SourceCodeKind.Regular);

        _compilationOptions = new CSharpCompilationOptions(
            OutputKind.ConsoleApplication,
            optimizationLevel: OptimizationLevel.Release,
            allowUnsafe: false,
            concurrentBuild: false,     // WASM is single-threaded; no point in parallel builds.
            nullableContextOptions: NullableContextOptions.Enable);
    }

    public async Task<CodeRunResponse> RunAsync(string code, CancellationToken ct = default)
    {
        var stopwatch = Stopwatch.StartNew();
        var rawSource = UsingPreamble + code;

        SyntaxTree tree;
        try
        {
            // Regular C# source files require every top-level statement to appear BEFORE
            // any namespace/type declaration (CS8803). CSharpScript was lax about this;
            // lesson starter code was written for the scripting relaxation, so we do a
            // two-step parse to rewrite whatever order the user supplied into the legal one.
            var rewritten = ReorderForRegularMode(rawSource);
            tree = CSharpSyntaxTree.ParseText(rewritten, _parseOptions, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            return Fail(stopwatch, $"Parse error: {ex.Message}");
        }

        var compilation = CSharpCompilation.Create(
            assemblyName: $"UserCode_{Guid.NewGuid():N}",
            syntaxTrees: [tree],
            references: _references,
            options: _compilationOptions);

        using var peStream = new MemoryStream();
        var emitResult = compilation.Emit(peStream, cancellationToken: ct);

        if (!emitResult.Success)
        {
            var diagnostics = emitResult.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => d.ToString());
            return Fail(stopwatch, "Compilation error:\n" + string.Join("\n", diagnostics));
        }

        peStream.Seek(0, SeekOrigin.Begin);

        Assembly userAssembly;
        try
        {
            userAssembly = Assembly.Load(peStream.ToArray());
        }
        catch (Exception ex)
        {
            return Fail(stopwatch, $"Load error: {ex.Message}");
        }

        // For async top-level statements, the compiler generates BOTH:
        //   1. `<Main>$(string[] args)` returning Task      — the real user code
        //   2. `Main(string[] args)` returning void         — a sync wrapper that blocks
        //      on the Task via `.GetAwaiter().GetResult()`
        // `Assembly.EntryPoint` points at the sync wrapper, and invoking it in single-
        // threaded WASM throws "Cannot wait on monitors on this runtime." So hunt for the
        // async version by name and await it directly.
        var asyncMain = userAssembly.GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            .FirstOrDefault(m => m.Name == "<Main>$" && typeof(Task).IsAssignableFrom(m.ReturnType));

        var entryPoint = asyncMain ?? userAssembly.EntryPoint;
        if (entryPoint is null)
        {
            return Fail(stopwatch, "No entry point found. Make sure the snippet has top-level statements.");
        }

        // Capture whatever the snippet writes to Console.
        var originalOut = Console.Out;
        var captured = new StringBuilder();
        using var writer = new StringWriter(captured);
        Console.SetOut(writer);

        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            // async Main generated from top-level statements has no parameters or a single
            // string[] parameter depending on C# version. Handle both.
            var args = entryPoint.GetParameters().Length == 0
                ? null
                : new object[] { Array.Empty<string>() };

            var result = entryPoint.Invoke(null, args);
            if (result is Task task)
            {
                await task.WaitAsync(cts.Token);
            }

            stopwatch.Stop();
            return new CodeRunResponse(
                Success: true,
                Output: captured.ToString(),
                Error: null,
                ElapsedMs: stopwatch.ElapsedMilliseconds);
        }
        catch (TargetInvocationException tie)
        {
            // Reflection wraps exceptions thrown by invoked methods — unwrap for nicer output.
            stopwatch.Stop();
            var inner = tie.InnerException ?? tie;
            return new CodeRunResponse(
                Success: false,
                Output: captured.ToString(),
                Error: $"{inner.GetType().Name}: {inner.Message}",
                ElapsedMs: stopwatch.ElapsedMilliseconds);
        }
        catch (OperationCanceledException)
        {
            stopwatch.Stop();
            return new CodeRunResponse(
                Success: false,
                Output: captured.ToString(),
                Error: "Execution timed out after 5 seconds. Check for infinite loops.",
                ElapsedMs: stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            return new CodeRunResponse(
                Success: false,
                Output: captured.ToString(),
                Error: $"{ex.GetType().Name}: {ex.Message}",
                ElapsedMs: stopwatch.ElapsedMilliseconds);
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }

    private static CodeRunResponse Fail(Stopwatch sw, string error)
    {
        sw.Stop();
        return new CodeRunResponse(false, "", error, sw.ElapsedMilliseconds);
    }

    /// <summary>
    /// Pulls apart a source file into (usings, global statements, type declarations)
    /// and concatenates them in the order regular C# source mode requires. We parse in
    /// Regular mode — even when the original ordering is illegal (CS8803), the syntax
    /// tree still keeps the correct node SHAPE, and errors are only attached afterwards.
    ///
    /// Script mode was tempting because it's lenient about order, but it reinterprets
    /// top-level variable declarations as script fields (not GlobalStatements), which
    /// would wreck our bucketing.
    /// </summary>
    private string ReorderForRegularMode(string source)
    {
        var tree = CSharpSyntaxTree.ParseText(source, _parseOptions);
        if (tree.GetRoot() is not CompilationUnitSyntax root) return source;

        var statements = new StringBuilder();
        var types = new StringBuilder();
        foreach (var member in root.Members)
        {
            if (member is GlobalStatementSyntax)
            {
                statements.Append(member.ToFullString());
            }
            else
            {
                types.Append(member.ToFullString());
            }
        }

        // If it's already pure statements or pure types, leave it alone.
        if (statements.Length == 0 || types.Length == 0) return source;

        var sb = new StringBuilder();
        foreach (var u in root.Usings)
        {
            sb.Append(u.ToFullString());
        }
        sb.AppendLine();
        sb.Append(statements);
        sb.AppendLine();
        sb.Append(types);
        return sb.ToString();
    }
}
