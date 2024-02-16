using LinqToDB.Mapping;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
namespace TaskEvaluator.Sinks.Database.DataModel;

public sealed class DbStaticCodeResult {
    [PrimaryKey(1), Identity]
    public Guid TaskId { get; init; }

    [PrimaryKey(2), Identity]
    public string CodeId { get; init; } = string.Empty;

    [Column, NotNull]
    public Severity Severity { get; init; }

    [Column, NotNull]
    public string QualityAttribute { get; init; } = string.Empty;

    [Column, NotNull]
    public string QualityMetric { get; init; } = string.Empty;

    [Column, NotNull]
    public int Line { get; init; }

    [Column]
    public string? Context { get; init; }
}
