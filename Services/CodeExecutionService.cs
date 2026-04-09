using System.Diagnostics;
using System.Text;
using CSharpLearningLab.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CSharpLearningLab.Services;

/// <summary>
/// Executes user-submitted C# snippets inside the current process using Roslyn's
/// scripting API. Captures stdout so the frontend can display what the snippet printed.
///
/// Note: this runs in-process with full trust. It is intended for LOCAL learning only.
/// Do NOT expose this endpoint to the public internet.
/// </summary>
public sealed class CodeExecutionService
{
    private static readonly ScriptOptions DefaultOptions = ScriptOptions.Default
        .AddImports(
            "System",
            "System.IO",
            "System.Text",
            "System.Linq",
            "System.Collections.Generic",
            "System.Threading",
            "System.Threading.Tasks"
        )
        // Reference every assembly already loaded in the host process. This is by far the
        // simplest way to give user scripts access to the full BCL (Console, List<T>, LINQ,
        // Task, etc.) without manually enumerating each one. Safe because the playground
        // is local-only and single-user.
        .AddReferences(
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
        );

    public async Task<CodeRunResponse> RunAsync(string code, CancellationToken ct = default)
    {
        var stopwatch = Stopwatch.StartNew();
        var originalOut = Console.Out;
        var capturedOut = new StringBuilder();
        var writer = new StringWriter(capturedOut);

        Console.SetOut(writer);
        try
        {
            // Give the user a 5-second budget per run so runaway loops don't freeze the server.
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            var state = await CSharpScript.RunAsync(code, DefaultOptions, cancellationToken: cts.Token);
            stopwatch.Stop();

            // If the snippet's last expression returned a value (e.g. `1 + 1`), surface it.
            if (state.ReturnValue is not null)
            {
                capturedOut.Append(state.ReturnValue);
            }

            return new CodeRunResponse(
                Success: true,
                Output: capturedOut.ToString(),
                Error: null,
                ElapsedMs: stopwatch.ElapsedMilliseconds
            );
        }
        catch (CompilationErrorException ex)
        {
            stopwatch.Stop();
            return new CodeRunResponse(
                Success: false,
                Output: capturedOut.ToString(),
                Error: "Compilation error:\n" + string.Join("\n", ex.Diagnostics),
                ElapsedMs: stopwatch.ElapsedMilliseconds
            );
        }
        catch (OperationCanceledException)
        {
            stopwatch.Stop();
            return new CodeRunResponse(
                Success: false,
                Output: capturedOut.ToString(),
                Error: "Execution timed out after 5 seconds. Check for infinite loops.",
                ElapsedMs: stopwatch.ElapsedMilliseconds
            );
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            return new CodeRunResponse(
                Success: false,
                Output: capturedOut.ToString(),
                Error: $"{ex.GetType().Name}: {ex.Message}",
                ElapsedMs: stopwatch.ElapsedMilliseconds
            );
        }
        finally
        {
            Console.SetOut(originalOut);
            writer.Dispose();
        }
    }
}
