using TaskEvaluator.Runtime;
namespace TaskEvaluator.Language.Implementations.CSharp;

public sealed record CSharpRuntimeResult(bool Success, object? ReturnValue) : IRuntimeResult;
