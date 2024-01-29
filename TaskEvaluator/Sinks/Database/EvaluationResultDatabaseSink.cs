using LinqToDB;
using LinqToDB.Data;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Sinks.Database.DataModel;
namespace TaskEvaluator.Sinks.Database;

public sealed record EvaluationResultDatabaseSinkConfiguration(string ConnectionString);

public sealed class EvaluationResultDatabase(string connectionString) : DataConnection(options => options.UsePostgreSQL(connectionString)) {
    public ITable<StaticCodeEvaluationResult> StaticCodeResults => this.GetOrCreateTable<StaticCodeEvaluationResult>();

    public ITable<L2DbUnitTestEvaluationResult> UnitTestEvaluationResult => this.GetOrCreateTable<L2DbUnitTestEvaluationResult>();
    public ITable<L2DbUnitTestResult> UnitTestResults => this.GetOrCreateTable<L2DbUnitTestResult>();
}

public static class DataContextExtensions {
    public static ITable<T> GetOrCreateTable<T>(this DataConnection db) where T : class {
        try {
            db.CreateTable<T>();
        } catch (Exception e) {
            // ignored
        }

        return db.GetTable<T>();
    }
}

public sealed class EvaluationResultDatabaseSink(EvaluationResultDatabaseSinkConfiguration config) : IEvaluationResultSink {
    public void Send(IEvaluationResult evaluationResult) {
        using var db = new EvaluationResultDatabase(config.ConnectionString);
        switch (evaluationResult) {
            case StaticCodeEvaluationResult result:
                db.StaticCodeResults.Insert(() => result);
                break;
            case UnitTestEvaluationResult result:
                db.UnitTestEvaluationResult.Insert(() => new L2DbUnitTestEvaluationResult {
                    TaskId = result.TaskId,
                    Success = result.Success,
                    Context = result.Context
                });

                foreach (var unitTestResult in result.Results) {
                    db.UnitTestResults.Insert(() => new L2DbUnitTestResult {
                        TaskId = result.TaskId,
                        TestName = unitTestResult.TestName,
                        Outcome = unitTestResult.Outcome,
                        Duration = unitTestResult.Duration
                    });
                }

                break;
        }
    }
}
