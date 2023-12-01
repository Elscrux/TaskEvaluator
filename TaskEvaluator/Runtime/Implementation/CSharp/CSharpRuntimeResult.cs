namespace TaskEvaluator.Runtime.Implementation.CSharp;

public sealed record CSharpRuntimeResult(bool Success, object? ReturnValue) : IRuntimeResult;
