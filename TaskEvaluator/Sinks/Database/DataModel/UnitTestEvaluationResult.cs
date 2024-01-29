using LinqToDB.Mapping;
using TaskEvaluator.Evaluator;
namespace TaskEvaluator.Sinks.Database.DataModel;

public sealed class L2DbUnitTestEvaluationResult : IEvaluationResult {
    [PrimaryKey, Identity]
    public Guid TaskId { get; init; }

    [Column, NotNull]
    public bool Success { get; init; }

    [Column]
    public string? Context { get; init; }

    [Association(ThisKey = nameof(TaskId), OtherKey = nameof(L2DbUnitTestResult.TaskId))]
    public IList<L2DbUnitTestResult> Results { get; init; } = [];
}
