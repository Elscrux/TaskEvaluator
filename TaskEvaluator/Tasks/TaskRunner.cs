using System.Runtime.CompilerServices;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Tasks;

public sealed class TaskRunner(
    LanguageFactory languageFactory,
    TaskRetry retry,
    IEvaluatorProvider evaluatorProvider,
    ICodeGenerationProvider codeGenerationProvider) {

    public async IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, [EnumeratorCancellation] CancellationToken token = default) {
        using var runtime = await languageFactory.CreateRuntime(code, token);

        var evaluationTasks = await evaluatorProvider
            .GetEvaluators(evaluationModel, runtime)
            .Select(evaluator => evaluator.Evaluate(code, evaluationModel, token))
            .ToListAsync(token);

        await foreach (var result in evaluationTasks.AwaitAll(token)) {
            yield return result;
        }
    }

    public async IAsyncEnumerable<CodeGenerationResult> Generate(CodeGenerationTask task, [EnumeratorCancellation] CancellationToken token = default) {
        var models = codeGenerationProvider.GetGenerators().ToList();

        var tasks = models
            .Select(model => model.Send(task, token))
            .ToList();

        await foreach (var result in tasks.AwaitAll(token)) yield return result;
    }

    public async IAsyncEnumerable<FinalResult> Process(TaskSet taskSet, [EnumeratorCancellation] CancellationToken token = default) {
        var finalResults = await retry.Try(taskSet, async task => {
            var tasks = await Generate(task, token)
                .Where(result => result.Success)
                .Select(codeGenerationResult => {
                    return Task.Run(async () => {
                        var successfulRevaluationResults = await Evaluate(codeGenerationResult.Code, taskSet.EvaluationModel, token)
                            .Where(result => result.Success)
                            .ToListAsync(token);

                        return new FinalResult(codeGenerationResult, successfulRevaluationResults);
                    }, token);
                })
                .ToListAsync(token);

            return await Task.WhenAll(tasks);
        });

        foreach (var finalResult in finalResults) {
            yield return finalResult;
        }
    }
}
