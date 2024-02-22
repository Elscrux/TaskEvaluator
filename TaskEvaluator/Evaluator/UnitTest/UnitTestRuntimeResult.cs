using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestRuntimeResult(
    bool Success,
    string Evaluator,
    string? Context,
    IReadOnlyList<UnitTestResult> Results)
    : IRuntimeResult {

    public static UnitTestRuntimeResult Successful(string? context, string evaluator, IReadOnlyList<UnitTestResult> results)
        => new(true, evaluator, context, results);
    public static UnitTestRuntimeResult Failure(string? context) => new(false, string.Empty, context, []);

    public UnitTestRuntimeResult() : this(false, string.Empty, null, []) {}
}
