using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator.StaticCodeAnalysis;

public sealed record StaticCodeRuntimeResult(bool Success, string? Context, IReadOnlyList<StaticCodeResult> Results) : IRuntimeResult {
    public StaticCodeRuntimeResult() : this(false, null, []) {}
    public StaticCodeRuntimeResult(bool success, string? context) : this(success, context, []) {}
    public StaticCodeRuntimeResult(StaticCodeEvaluationResult staticCodeEvaluationResult) : this(
        staticCodeEvaluationResult.Success,
        staticCodeEvaluationResult.Context,
        staticCodeEvaluationResult.Results) {}
}
