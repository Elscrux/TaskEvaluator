using LinqToDB.Mapping;
using TaskEvaluator.Evaluator;
namespace TaskEvaluator.Sink.PostgreSQL.DataModel;

public sealed class DbSyntaxValidationResult : IEvaluationResult {
    [PrimaryKey, Identity]
    public Guid CodeId { get; init; }

    [Column, NotNull]
    public bool Success { get; init; }

    [Column, NotNull]
    public string Evaluator { get; init; } = string.Empty;

    [Column]
    public string? Context { get; init; }

    [Column, NotNull]
    public bool SyntaxValid { get; init; }

    public bool IsValid() => true;
}
