using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestEvaluationResult(Guid Guid, bool Success) : IEvaluationResult {
    public UnitTestEvaluationResult(Guid guid, IRuntimeResult runtimeResult) : this(guid, runtimeResult.Success) {}
}
