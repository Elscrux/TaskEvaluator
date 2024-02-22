using System.Runtime.CompilerServices;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Tasks;

public sealed class TaskRunner(
    LanguageFactory languageFactory,
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
        var listAsync = await Generate(taskSet.CodeGenerationTask, token)
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

        await foreach (var finalResult in listAsync.AwaitAll(token)) {
            yield return finalResult;
        }
    }
}
