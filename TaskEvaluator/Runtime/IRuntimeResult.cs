namespace TaskEvaluator.Runtime;

public interface IRuntimeResult {
    bool Success { get; }
    object? ReturnValue { get; }
}
