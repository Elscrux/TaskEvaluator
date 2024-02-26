namespace TaskEvaluator.Evaluator.SyntaxValidation;

public sealed record SyntaxValidationResult(Guid CodeId, bool Success, string Evaluator, string? Context, bool SyntaxValid) : IEvaluationResult {
    public SyntaxValidationResult(Guid codeId, SyntaxValidationRuntimeResult runtimeResult) : this(
        codeId,
        runtimeResult.Success,
        runtimeResult.Evaluator,
        runtimeResult.Context,
        runtimeResult.SyntaxValid) {}

    public bool IsValid() => SyntaxValid;
}
