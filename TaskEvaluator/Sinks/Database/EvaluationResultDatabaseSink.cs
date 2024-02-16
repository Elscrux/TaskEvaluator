using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Options;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Sinks.Database.DataModel;
namespace TaskEvaluator.Sinks.Database;

public sealed record EvaluationResultDatabaseSinkConfiguration {
    public required string ConnectionString { get; init; }
}

public sealed class EvaluationResultDatabase(string connectionString) : DataConnection(options => options.UsePostgreSQL(connectionString)) {
    public ITable<DbStaticCodeAnalysisEvaluationResult> StaticCodeEvaluationResult => this.GetOrCreateTable<DbStaticCodeAnalysisEvaluationResult>();
    public ITable<DbStaticCodeResult> StaticCodeResults => this.GetOrCreateTable<DbStaticCodeResult>();

    public ITable<DbUnitTestEvaluationResult> UnitTestEvaluationResult => this.GetOrCreateTable<DbUnitTestEvaluationResult>();
    public ITable<DbUnitTestResult> UnitTestResults => this.GetOrCreateTable<DbUnitTestResult>();
}

public static class DataContextExtensions {
    public static ITable<T> GetOrCreateTable<T>(this DataConnection db) where T : class {
        try {
            db.CreateTable<T>();
        } catch (Exception) {
            // ignored
        }

        return db.GetTable<T>();
    }
}

public sealed class EvaluationResultDatabaseSink(IOptions<EvaluationResultDatabaseSinkConfiguration> config) : IEvaluationResultSink {
    public void Send(IEvaluationResult evaluationResult) {
        if (!evaluationResult.Success) return;

        using var db = new EvaluationResultDatabase(config.Value.ConnectionString);
        switch (evaluationResult) {
            case StaticCodeEvaluationResult result:
                db.StaticCodeEvaluationResult.Insert(() => new DbStaticCodeAnalysisEvaluationResult {
                    TaskId = result.TaskId,
                    Success = result.Success,
                    Context = result.Context,
                });

                foreach (var codeResult in result.Results) {
                    db.StaticCodeResults.Insert(() => new DbStaticCodeResult {
                        TaskId = result.TaskId,
                        CodeId = codeResult.Id,
                        Severity = codeResult.Severity,
                        QualityAttribute = codeResult.QualityAttribute,
                        QualityMetric = codeResult.QualityMetric,
                        Line = codeResult.Line,
                        Context = codeResult.Context,
                    });
                }

                break;
            case UnitTestEvaluationResult result:
                db.UnitTestEvaluationResult.Insert(() => new DbUnitTestEvaluationResult {
                    TaskId = result.TaskId,
                    Success = result.Success,
                    Context = result.Context
                });

                foreach (var unitTestResult in result.Results) {
                    db.UnitTestResults.Insert(() => new DbUnitTestResult {
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
