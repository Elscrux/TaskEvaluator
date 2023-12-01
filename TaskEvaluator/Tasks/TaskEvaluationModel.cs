namespace TaskEvaluator.Tasks;

public sealed record TaskEvaluationModel(
    Solution Solution,
    IList<Code> UnitTests);
