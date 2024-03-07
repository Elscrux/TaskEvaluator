using LinqToDB.Mapping;
using TaskEvaluator.Language;
namespace TaskEvaluator.Sink.PostgreSQL.DataModel;

public sealed class DbCodeGenerationResult {
    [PrimaryKey, Identity]
    public Guid CodeId { get; init; }

    [Column]
    public Guid TaskId { get; init; }

    [Column]
    public string Body { get; init; } = string.Empty;

    [Column]
    public string GeneratedPart { get; init; } = string.Empty;

    [Column]
    public ProgrammingLanguage Language { get; init; }

    [Column]
    public string Generator { get; set; } = string.Empty;

    [Column]
    public int GenerationTimeMilliseconds { get; set; }
}
