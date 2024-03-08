using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.SyntaxValidation;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Generation;
using TaskEvaluator.Sink.PostgreSQL.DataModel;
using TaskEvaluator.Sinks;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Sink.PostgreSQL;

public sealed record PostgresSinkConfiguration {
    public required string ConnectionString { get; init; }
}

public sealed class ResultsDatabase(string connectionString) : DataConnection(options => options.UsePostgreSQL(connectionString)) {
    public IEnumerable<IExpressionQuery> GetTables() {
        yield return CodeGenerationResults;
        yield return StaticCodeResults;
        yield return StaticCodeEvaluationResult;
        yield return UnitTestResults;
        yield return UnitTestEvaluationResult;
        yield return SyntaxValidationResults;
    } 

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

public sealed class PostgresFinalResultSink : IFinalResultSink {
    private readonly ILogger<PostgresFinalResultSink> _logger;
    private readonly IOptions<PostgresSinkConfiguration> _config;

    public PostgresFinalResultSink(ILogger<PostgresFinalResultSink> logger,
        IOptions<PostgresSinkConfiguration> config) {
        _logger = logger;
        _config = config;

        using var db = new ResultsDatabase(_config.Value.ConnectionString);
        foreach (var unused in db.GetTables()) {
            // Ensure that the tables are created
        }
    }

    public void Send(FinalResult finalResult) {
        using var db = new ResultsDatabase(_config.Value.ConnectionString);

        if (db.CodeGenerationResults.Any(x => x.CodeId == finalResult.CodeGenerationResult.Code.Guid)) {
            _logger.LogWarning("Code with id {CodeId} already exists in the database, aborting insertion", finalResult.CodeGenerationResult.Code.Guid);
            return;
        }

        db.CodeGenerationResults.Insert(() => new DbCodeGenerationResult {
            CodeId = finalResult.CodeGenerationResult.Code.Guid,
            TaskId = finalResult.CodeGenerationResult.TaskId,
            Body = finalResult.CodeGenerationResult.Code.Body,
            Language = finalResult.CodeGenerationResult.Code.Language,
            Generator = finalResult.CodeGenerationResult.Generator,
            GeneratedPart = finalResult.CodeGenerationResult.GeneratedPart,
            GenerationTimeMilliseconds = finalResult.CodeGenerationResult.GenerationTime.Milliseconds,
            RetryCount = finalResult.CodeGenerationResult.RetryCount,
        });

        foreach (var evaluationResult in finalResult.EvaluationResults) {
            StoreEvaluationResult(db, evaluationResult);
        }
    }

    public IEnumerable<FinalResult> Retrieve() {
        using var db = new ResultsDatabase(_config.Value.ConnectionString);
        if (db is null) throw new InvalidOperationException("Database is null");

        var codeGenerationResults = db.CodeGenerationResults
            .Select(x => new CodeGenerationResult(
                x.TaskId,
                true,
                new Code(x.CodeId, x.Body, x.Language),
                x.GeneratedPart,
                x.Generator,
                TimeSpan.FromMilliseconds(x.GenerationTimeMilliseconds),
                x.RetryCount
            ))
            .ToList();

        var syntaxValidationResults = db.SyntaxValidationResults
            .Select(x => new SyntaxValidationResult(
                x.CodeId,
                x.Success,
                x.Evaluator,
                x.Context,
                x.SyntaxValid
            ))
            .ToList();

        var unitTestEvaluationResults = db.UnitTestEvaluationResult
            .Select(x => new UnitTestEvaluationResult(
                x.CodeId,
                x.Success,
                x.Evaluator,
                x.Context,
                db.UnitTestResults
                    .Where(z => z.CodeId == x.CodeId)
                    .Select(z => new UnitTestResult(
                        z.TestName,
                        z.Outcome,
                        z.Duration
                    ))
                    .ToList()
            ))
            .ToList();

        var staticCodeEvaluationResults = db.StaticCodeEvaluationResult
            .Select(x => new StaticCodeEvaluationResult(
                x.CodeId,
                x.Success,
                x.Evaluator,
                x.Context,
                db.StaticCodeResults
                    .Where(z => z.CodeId == x.CodeId)
                    .Select(z => new StaticCodeResult(
                        z.CodeAnalysisId,
                        z.Context,
                        z.Severity,
                        z.QualityAttribute,
                        z.QualityMetric,
                        z.Line,
                        new Dictionary<string, object>()
                    ))
                    .ToList()
            ))
            .ToList();

        var evaluationResults = syntaxValidationResults
            .Concat<IEvaluationResult>(unitTestEvaluationResults)
            .Concat(staticCodeEvaluationResults).ToList();

        foreach (var result in codeGenerationResults) {
            var rightResults = evaluationResults
                .Where(x => result.Code.Guid == x.CodeId)
                .ToList();

            yield return new FinalResult(result, rightResults);
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
