namespace TaskEvaluator.Evaluator.StaticCodeAnalysis;

public sealed record StaticCodeEvaluationResult(
    Guid TaskId,
    bool Success,
    string? Context,
    IReadOnlyList<StaticCodeResult> Results) : IEvaluationResult {
    public StaticCodeEvaluationResult(Guid taskId, StaticCodeRuntimeResult staticCodeRuntimeResult) : this(
        taskId,
        staticCodeRuntimeResult.Success,
        staticCodeRuntimeResult.Context,
        staticCodeRuntimeResult.Results) {}
}
