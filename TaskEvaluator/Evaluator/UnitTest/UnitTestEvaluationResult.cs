namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestEvaluationResult(Guid Guid, bool Success, string? Context, IList<UnitTestResult> Results) : IEvaluationResult {
    public UnitTestEvaluationResult(Guid guid, bool success, string? context) : this(
        guid,
        success,
        context,
        Array.Empty<UnitTestResult>()) {}

    public UnitTestEvaluationResult(Guid guid, UnitTestRuntimeResult unitTestRuntimeResult) : this(
        guid,
        unitTestRuntimeResult.Success,
        unitTestRuntimeResult.Context,
        unitTestRuntimeResult.Results) {}
}
