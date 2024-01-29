namespace TaskEvaluator.Evaluator;

public interface IEvaluationResult {
    Guid TaskId { get; }
    bool Success { get; }
    string? Context { get; }
}
