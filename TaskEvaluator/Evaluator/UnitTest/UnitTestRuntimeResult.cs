using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestRuntimeResult(bool Success, string? Context, IList<UnitTestResult> Results) : IRuntimeResult {
    public UnitTestRuntimeResult() : this(false, null, []) {}
    public UnitTestRuntimeResult(bool success, string? context) : this(success, context, []) {}
}
