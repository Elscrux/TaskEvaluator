using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator.StaticCodeAnalysis;

public sealed record StaticCodeRuntimeResult(
    bool Success,
    string Evaluator,
    string? Context,
    IReadOnlyList<StaticCodeResult> Results)
    : IRuntimeResult {

    public static StaticCodeRuntimeResult Successful(string? context, string evaluator, IReadOnlyList<StaticCodeResult> results)
        => new(true, evaluator, context, results);
    public static StaticCodeRuntimeResult Failure(string? context) => new(false, string.Empty, context, []);

    public StaticCodeRuntimeResult() : this(false, string.Empty, null, []) {}
    public StaticCodeRuntimeResult(StaticCodeEvaluationResult staticCodeEvaluationResult) : this(
        staticCodeEvaluationResult.Success,
        staticCodeEvaluationResult.Evaluator,
        staticCodeEvaluationResult.Context,
        staticCodeEvaluationResult.Results) {}
}
