using LinqToDB.Mapping;
using TaskEvaluator.Evaluator.UnitTest;
namespace TaskEvaluator.Sink.PostgreSQL.DataModel;

public sealed class DbUnitTestResult {
    [PrimaryKey(1), Identity]
    public Guid CodeId { get; init; }

    [PrimaryKey(2), Identity]
    public string TestName { get; init; } = string.Empty;

    [Column]
    public UnitTestOutcome Outcome { get; init; }

    [Column]
    public TimeSpan Duration { get; init; }
}
