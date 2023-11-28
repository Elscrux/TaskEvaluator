using TaskEvaluator.Evaluator;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Task;

public sealed class TaskRunner(
    RuntimeService runtimeService,
    LocalTaskProvider localTaskProvider,
    IEvaluatorProvider evaluatorProvider) {

    public IEnumerable<IEvaluationResult> RunLocal() {
        return localTaskProvider
            .GetTasks()
            .SelectMany(Run);
    }

    public IEnumerable<IEvaluationResult> Run(TaskEvaluationModel model) {
        var runtime = runtimeService.CreateRuntime(model.Task.Code);

        foreach (var evaluator in evaluatorProvider.GetEvaluators(model)) {
            foreach (var evaluationResult in evaluator.Evaluate(model, runtime)) {
                yield return evaluationResult;
            }
        }
    }
}
