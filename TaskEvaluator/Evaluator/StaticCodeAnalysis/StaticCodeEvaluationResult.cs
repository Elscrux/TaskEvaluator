namespace TaskEvaluator.Evaluator.StaticCodeAnalysis;

public sealed record StaticCodeEvaluationResult(
    Guid CodeId,
    bool Success,
    string Evaluator,
    string? Context,
    IReadOnlyList<StaticCodeResult> Results) : IEvaluationResult {
    public static StaticCodeEvaluationResult Successful(
        Guid codeId,
        string evaluator,
        string? context,
        IReadOnlyList<StaticCodeResult> results) => new(codeId, true, evaluator, context, results);
    public static StaticCodeEvaluationResult Failure(Guid codeId, string evaluator, string? context) => new(codeId, false, evaluator, context, []);

    public StaticCodeEvaluationResult(Guid codeId, StaticCodeRuntimeResult staticCodeRuntimeResult) : this(
        codeId,
        staticCodeRuntimeResult.Success,
        staticCodeRuntimeResult.Evaluator,
        staticCodeRuntimeResult.Context,
        staticCodeRuntimeResult.Results) {}

    public bool IsValid() => Results.Count == 0;
}
