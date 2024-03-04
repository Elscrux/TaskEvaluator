using Microsoft.Extensions.Logging;
using Noggog;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.SyntaxValidation;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Generation;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Tasks;

public sealed class TaskRetry(
    ILogger<TaskRetry> logger,
    LanguageFactory languageFactory) {
    private const int DefaultTries = 3;

    public async Task<IReadOnlyList<FinalResult>> Try(TaskSet taskSet, Func<CodeGenerationTask, Task<IReadOnlyList<FinalResult>>> retryFunction) {
        var set = taskSet;
        var languageService = languageFactory.GetLanguageService(taskSet.CodeGenerationTask.Language);

        for (var i = 0; i < DefaultTries; i++) {
            var results = await retryFunction(taskSet.CodeGenerationTask);
            var hint = results
                .Select(GetFirstHint)
                .NotNull()
                .FirstOrDefault();

            if (hint is null) return results;

            var commentedLines = hint
                .Split("\n")
                .Select(line => languageService.LineCommentSymbol + line);

            var commentedHint = string.Join("\n", commentedLines) + "\n\n";
            taskSet = taskSet with {
                CodeGenerationTask = taskSet.CodeGenerationTask with {
                    Prefix = commentedHint + taskSet.CodeGenerationTask.Prefix
                }
            };
        }

        logger.LogWarning("Failed to generate fully valid code for {Task} after {Tries} tries", taskSet.CodeGenerationTask, DefaultTries);
        return [];

        string? GetFirstHint(FinalResult finalResult) {
            return finalResult.EvaluationResults
                .Select(evaluationResult => GetHint(finalResult.CodeGenerationResult, evaluationResult))
                .NotNull()
                .FirstOrDefault();
        }

        string? GetHint(CodeGenerationResult codeGenerationResult, IEvaluationResult result) {
            switch (result) {
                case SyntaxValidationResult syntaxValidationResult:
                    if (!syntaxValidationResult.SyntaxValid) {
                        return "Avoid this syntax error:\n"
                          + syntaxValidationResult.Context
                          + "\n\nWhich this code has:\n" + codeGenerationResult.Code.Body
                          + "\n\n" + languageService.GetSyntaxErrorHint(codeGenerationResult, syntaxValidationResult);
                    }
                    break;
                case StaticCodeEvaluationResult staticCodeEvaluationResult:
                    if (staticCodeEvaluationResult.Results.Count > 0) {
                        return "Avoid these code smells:\n" + string.Join("\n\n", staticCodeEvaluationResult.Results);
                    }
                    break;
                case UnitTestEvaluationResult unitTestEvaluationResult:
                    var failedTests = unitTestEvaluationResult.Results
                        .Where(x => x.Outcome == UnitTestOutcome.Failed)
                        .ToList();

                    if (failedTests.Count > 0) {
                        return "Keep these test cases in mind, they failed:\n"
                          + string.Join("\n", failedTests.Select(x => x.TestName))
                          + "\n\nHere are the unit tests:\n"
                          + string.Join("\n\n", set.EvaluationModel.UnitTests);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result));
            }

            return null;
        }
    }
}
