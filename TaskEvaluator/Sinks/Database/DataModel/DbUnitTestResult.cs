using LinqToDB.Mapping;
using TaskEvaluator.Evaluator.UnitTest;
namespace TaskEvaluator.Sinks.Database.DataModel;

public sealed class DbUnitTestResult {
    [PrimaryKey(1), Identity]
    public Guid TaskId { get; init; }

    [PrimaryKey(2), Identity]
    public string TestName { get; init; } = string.Empty;

    [Column]
    public UnitTestOutcome Outcome { get; init; }

    [Column]
    public TimeSpan Duration { get; init; }
}
