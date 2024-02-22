using LinqToDB.Mapping;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
namespace TaskEvaluator.Sink.PostgreSQL.DataModel;

public sealed class DbStaticCodeAnalysisResult {
    [PrimaryKey(1), Identity]
    public Guid CodeId { get; init; }

    [PrimaryKey(2), Identity]
    public string CodeAnalysisId { get; init; } = string.Empty;

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
