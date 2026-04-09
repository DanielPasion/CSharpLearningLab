using CSharpLearningLab.Data;
using CSharpLearningLab.Models;
using CSharpLearningLab.Services;

var builder = WebApplication.CreateBuilder(args);

// Single shared code runner — Roslyn scripting is thread-safe enough for our use case.
builder.Services.AddSingleton<CodeExecutionService>();

// JSON defaults are fine for this app (System.Text.Json, camelCase).
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

// Serve the static frontend (wwwroot) as the app's home.
app.UseDefaultFiles();
app.UseStaticFiles();

// ---- API endpoints -----------------------------------------------------------

app.MapGet("/api/lessons", () =>
    // Return a lightweight summary for the left-hand nav; full detail comes from /api/lessons/{id}.
    LessonContent.All
        .OrderBy(l => l.Order)
        .Select(l => new { l.Id, l.Order, l.Title, l.Category })
);

app.MapGet("/api/lessons/{id}", (string id) =>
{
    var lesson = LessonContent.All.FirstOrDefault(l => l.Id == id);
    return lesson is null ? Results.NotFound() : Results.Ok(lesson);
});

app.MapPost("/api/run", async (CodeRunRequest req, CodeExecutionService runner, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(req.Code))
    {
        return Results.BadRequest(new CodeRunResponse(false, "", "Code is empty.", 0));
    }

    var result = await runner.RunAsync(req.Code, ct);
    return Results.Ok(result);
});

// ---- Run ---------------------------------------------------------------------

// Listen on a fixed local port so the run instructions are predictable.
app.Run("http://localhost:5080");
