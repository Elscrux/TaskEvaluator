namespace TaskEvaluator.Runtime;

public sealed record UnitTestRuntimeResult(bool Success, string? Context, IList<UnitTestResult> Results) : IRuntimeResult {
    public UnitTestRuntimeResult() : this(false, null, []) {}
    public UnitTestRuntimeResult(bool success, string? context) : this(success, context, []) {}
}
public sealed record UnitTestResult(string TestName, UnitTestOutcome Outcome, TimeSpan Duration);

public enum UnitTestOutcome {
    Passed,
    Failed,
    Skipped,
}