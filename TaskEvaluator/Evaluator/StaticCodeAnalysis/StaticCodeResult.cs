namespace TaskEvaluator.Evaluator.StaticCodeAnalysis;

public sealed record StaticCodeResult(
    string Id,
    string? Context,
    Severity Severity,
    string QualityAttribute,
    string QualityMetric,
    int Line,
    Dictionary<string, object> AdditionalProperties);
