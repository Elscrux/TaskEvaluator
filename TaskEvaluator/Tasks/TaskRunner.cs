using System.Runtime.CompilerServices;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Tasks;

public sealed class TaskRunner(
    LanguageFactory languageFactory,
    LocalTaskProvider localTaskProvider,
    IEvaluatorProvider evaluatorProvider) {

    public async IAsyncEnumerable<IEvaluationResult> RunLocal([EnumeratorCancellation] CancellationToken token = default) {
        foreach (var model in localTaskProvider.GetTasks()) {
            await foreach (var result in Run(model, token)) {
                yield return result;
            }
        }
    }

    public async IAsyncEnumerable<IEvaluationResult> Run(TaskEvaluationModel model, [EnumeratorCancellation] CancellationToken token = default) {
        using var runtime = await languageFactory.CreateRuntime(model.Solution.Code, token);

        foreach (var evaluator in evaluatorProvider.GetEvaluators(model, runtime)) {
            await foreach (var evaluationResult in evaluator.Evaluate(model, token)) {
                yield return evaluationResult;
            }
        }
    }
}
