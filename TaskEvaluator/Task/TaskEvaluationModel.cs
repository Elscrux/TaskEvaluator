namespace TaskEvaluator.Task;

public sealed record TaskEvaluationModel(
    Task Task,
    IList<Code> UnitTests);
