namespace TaskEvaluator.Runtime;

public interface IRuntimeResult {
    bool Success { get; }
    string? Context { get; }
}
