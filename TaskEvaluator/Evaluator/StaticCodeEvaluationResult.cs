namespace TaskEvaluator.Evaluator;

public sealed record StaticCodeEvaluationResult(
    Guid TaskId,
    bool Success,
    string? Context,
    Severity Severity,
    string QualityAttribute,
    string QualityMetric,
    int Line,
    Dictionary<string, object> AdditionalProperties) : IEvaluationResult;
