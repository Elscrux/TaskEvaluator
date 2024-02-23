using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.SyntaxValidation;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Sink.PostgreSQL.DataModel;
using TaskEvaluator.Sinks;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Sink.PostgreSQL;

public sealed record PostgresSinkConfiguration {
    public required string ConnectionString { get; init; }
}

public sealed class ResultsDatabase(string connectionString) : DataConnection(options => options.UsePostgreSQL(connectionString)) {
    public ITable<DbCodeGenerationResult> CodeGenerationResults => this.GetOrCreateTable<DbCodeGenerationResult>();

    public ITable<DbStaticCodeAnalysisEvaluationResult> StaticCodeEvaluationResult => this.GetOrCreateTable<DbStaticCodeAnalysisEvaluationResult>();
    public ITable<DbStaticCodeAnalysisResult> StaticCodeResults => this.GetOrCreateTable<DbStaticCodeAnalysisResult>();

    public ITable<DbUnitTestEvaluationResult> UnitTestEvaluationResult => this.GetOrCreateTable<DbUnitTestEvaluationResult>();
    public ITable<DbUnitTestResult> UnitTestResults => this.GetOrCreateTable<DbUnitTestResult>();

    public ITable<DbSyntaxValidationResult> SyntaxValidationResults => this.GetOrCreateTable<DbSyntaxValidationResult>();
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

public sealed class PostgresFinalResultSink(
    ILogger<PostgresFinalResultSink> logger,
    IOptions<PostgresSinkConfiguration> config)
    : IFinalResultSink {
    public void Send(FinalResult finalResult) {
        using var db = new ResultsDatabase(config.Value.ConnectionString);

        if (db.CodeGenerationResults.Any(x => x.CodeId == finalResult.CodeGenerationResult.Code.Guid)) {
            logger.LogWarning("Code with id {CodeId} already exists in the database, aborting insertion", finalResult.CodeGenerationResult.Code.Guid);
            return;
        }

        db.CodeGenerationResults.Insert(() => new DbCodeGenerationResult {
            CodeId = finalResult.CodeGenerationResult.Code.Guid,
            Body = finalResult.CodeGenerationResult.Code.Body,
            Language = finalResult.CodeGenerationResult.Code.Language,
            Generator = finalResult.CodeGenerationResult.Generator,
        });

        foreach (var evaluationResult in finalResult.EvaluationResults) {
            StoreEvaluationResult(db, evaluationResult);
        }
    }

    private static void StoreEvaluationResult(ResultsDatabase db, IEvaluationResult evaluationResult) {
        switch (evaluationResult) {
            case StaticCodeEvaluationResult result:
                db.StaticCodeEvaluationResult.Insert(() => new DbStaticCodeAnalysisEvaluationResult {
                    CodeId = result.CodeId,
                    Success = result.Success,
                    Evaluator = result.Evaluator,
                    Context = result.Context,
                });

                foreach (var codeAnalysisResult in result.Results) {
                    db.StaticCodeResults.Insert(() => new DbStaticCodeAnalysisResult {
                        CodeId = result.CodeId,
                        CodeAnalysisId = codeAnalysisResult.Id,
                        Severity = codeAnalysisResult.Severity,
                        QualityAttribute = codeAnalysisResult.QualityAttribute,
                        QualityMetric = codeAnalysisResult.QualityMetric,
                        Line = codeAnalysisResult.Line,
                        Context = codeAnalysisResult.Context,
                    });
                }

                break;
            case UnitTestEvaluationResult result:
                db.UnitTestEvaluationResult.Insert(() => new DbUnitTestEvaluationResult {
                    CodeId = result.CodeId,
                    Success = result.Success,
                    Evaluator = result.Evaluator,
                    Context = result.Context
                });

                foreach (var unitTestResult in result.Results) {
                    db.UnitTestResults.Insert(() => new DbUnitTestResult {
                        CodeId = result.CodeId,
                        TestName = unitTestResult.TestName,
                        Outcome = unitTestResult.Outcome,
                        Duration = unitTestResult.Duration
                    });
                }

                break;
            case SyntaxValidationResult result:
                db.SyntaxValidationResults.Insert(() => new DbSyntaxValidationResult {
                    CodeId = result.CodeId,
                    Success = result.Success,
                    Evaluator = result.Evaluator,
                    Context = result.Context,
                    SyntaxValid = result.SyntaxValid,
                });

                break;
        }
    }
}
