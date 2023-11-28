namespace TaskEvaluator.Evaluator;

public interface IEvaluationResult {
    Guid Guid { get; }
    bool Success { get; }
}
