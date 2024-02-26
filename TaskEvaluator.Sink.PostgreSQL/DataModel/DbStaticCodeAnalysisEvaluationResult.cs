using LinqToDB.Mapping;
using TaskEvaluator.Evaluator;
namespace TaskEvaluator.Sink.PostgreSQL.DataModel;

public sealed class DbStaticCodeAnalysisEvaluationResult : IEvaluationResult {
    [PrimaryKey, Identity]
    public Guid CodeId { get; init; }

    [Column, NotNull]
    public bool Success { get; init; }

    [Column, NotNull]
    public string Evaluator { get; init; } = string.Empty;

    [Column]
    public string? Context { get; init; }

    [Association(ThisKey = nameof(CodeId), OtherKey = nameof(DbUnitTestResult.CodeId))]
    public IReadOnlyList<DbStaticCodeAnalysisResult> Results { get; init; } = [];

    public bool IsValid() => true;
}
