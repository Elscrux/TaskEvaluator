namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestEvaluationResult(Guid CodeId, bool Success, string Evaluator, string? Context, IReadOnlyList<UnitTestResult> Results) : IEvaluationResult {
    public static UnitTestEvaluationResult Successful(Guid codeId, string evaluator, string? context, IReadOnlyList<UnitTestResult> results)
        => new(codeId, true, evaluator, context, results);
    public static UnitTestEvaluationResult Failure(Guid codeId, string evaluator, string? context) => new(codeId, false, evaluator, context, Array.Empty<UnitTestResult>());

    public UnitTestEvaluationResult(Guid codeId, UnitTestRuntimeResult unitTestRuntimeResult) : this(
        codeId,
        unitTestRuntimeResult.Success,
        unitTestRuntimeResult.Evaluator,
        unitTestRuntimeResult.Context,
        unitTestRuntimeResult.Results) {}

    public bool IsValid() => Results.All(x => x.Outcome == UnitTestOutcome.Passed);
}
