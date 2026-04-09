using System.Text.Json;
using CSharpLearningLab.Data;
using CSharpLearningLab.Models;
using Microsoft.JSInterop;

namespace CSharpLearningLab.Services;

/// <summary>
/// The bridge the vanilla-JS frontend in <c>wwwroot/</c> talks to. Each method is marked
/// <see cref="JSInvokableAttribute"/> so it can be called from JavaScript via
/// <c>DotNet.invokeMethodAsync('CSharpLearningLab', 'MethodName', ...)</c>.
///
/// These used to be HTTP endpoints in the old minimal-API version. They return JSON
/// strings (rather than object graphs) to sidestep System.Text.Json's source generator
/// requirements under Blazor's default trimming configuration.
/// </summary>
public static class JsBridge
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    // The singleton CodeExecutionService is resolved from the host once, lazily.
    private static CodeExecutionService? _runner;

    internal static void Initialize(CodeExecutionService runner) => _runner = runner;

    /// <summary>Returns a JSON array of lesson summaries (id, order, title, category).</summary>
    [JSInvokable]
    public static string ListLessons()
    {
        var summaries = LessonContent.All
            .OrderBy(l => l.Order)
            .Select(l => new { l.Id, l.Order, l.Title, l.Category });
        return JsonSerializer.Serialize(summaries, JsonOptions);
    }

    /// <summary>Returns a single lesson (full detail) as a JSON object, or "null" if not found.</summary>
    [JSInvokable]
    public static string GetLesson(string id)
    {
        var lesson = LessonContent.All.FirstOrDefault(l => l.Id == id);
        return lesson is null ? "null" : JsonSerializer.Serialize(lesson, JsonOptions);
    }

    /// <summary>
    /// Compiles and executes a C# snippet via Roslyn, returning a JSON-serialized
    /// <see cref="CodeRunResponse"/>.
    /// </summary>
    [JSInvokable]
    public static async Task<string> RunCode(string code)
    {
        if (_runner is null)
        {
            var err = new CodeRunResponse(false, "", "Runtime not initialized.", 0);
            return JsonSerializer.Serialize(err, JsonOptions);
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            var err = new CodeRunResponse(false, "", "Code is empty.", 0);
            return JsonSerializer.Serialize(err, JsonOptions);
        }

        var result = await _runner.RunAsync(code);
        return JsonSerializer.Serialize(result, JsonOptions);
    }
}
