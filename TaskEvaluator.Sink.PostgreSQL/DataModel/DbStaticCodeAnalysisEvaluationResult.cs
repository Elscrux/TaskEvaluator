using LinqToDB.Mapping;
using TaskEvaluator.Evaluator;
namespace TaskEvaluator.Sinks.Database.DataModel;

public sealed class DbStaticCodeAnalysisEvaluationResult : IEvaluationResult {
    [PrimaryKey, Identity]
    public Guid TaskId { get; init; }

    [Column, NotNull]
    public bool Success { get; init; }

    [Column]
    public string? Context { get; init; }

    [Association(ThisKey = nameof(TaskId), OtherKey = nameof(DbUnitTestResult.TaskId))]
    public IReadOnlyList<DbStaticCodeResult> Results { get; init; } = [];
}
