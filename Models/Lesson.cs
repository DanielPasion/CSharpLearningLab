namespace CSharpLearningLab.Models;

public record Lesson(
    string Id,
    int Order,
    string Title,
    string Category,
    string TsAnalogy,
    string Explanation,
    string StarterCode,
    string SampleSolution,
    string ExpectedOutput,
    string[] KeyPoints,
    Quiz[] Quizzes
);

public record Quiz(
    string Question,
    string[] Choices,
    int CorrectIndex,
    string Explanation
);

public record CodeRunRequest(string Code);

public record CodeRunResponse(bool Success, string Output, string? Error, long ElapsedMs);
