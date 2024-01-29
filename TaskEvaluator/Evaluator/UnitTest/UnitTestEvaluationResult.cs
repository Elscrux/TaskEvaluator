namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestEvaluationResult(Guid TaskId, bool Success, string? Context, IList<UnitTestResult> Results) : IEvaluationResult {
    public UnitTestEvaluationResult(Guid taskId, bool success, string? context) : this(
        taskId,
        success,
        context,
        Array.Empty<UnitTestResult>()) {}

    public UnitTestEvaluationResult(Guid taskId, UnitTestRuntimeResult unitTestRuntimeResult) : this(
        taskId,
        unitTestRuntimeResult.Success,
        unitTestRuntimeResult.Context,
        unitTestRuntimeResult.Results) {}
}
