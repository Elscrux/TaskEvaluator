namespace TaskEvaluator.Evaluator.SyntaxValidation;

public sealed record SyntaxValidationRuntimeResult(bool Success, bool SyntaxValid, string Evaluator, string? Context) {
    public static SyntaxValidationRuntimeResult CodeValid(string evaluator, string? context) => new(true,true, evaluator, context);
    public static SyntaxValidationRuntimeResult CodeInvalid(string evaluator, string? context) => new(true,false, evaluator, context);
    public static SyntaxValidationRuntimeResult Failure(string? context) => new(false, false, string.Empty, context);
}
