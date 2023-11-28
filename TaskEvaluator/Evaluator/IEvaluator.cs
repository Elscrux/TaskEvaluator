using TaskEvaluator.Runtime;
using TaskEvaluator.Task;
namespace TaskEvaluator.Evaluator;

public interface IEvaluator {
    IEnumerable<IEvaluationResult> Evaluate(TaskEvaluationModel model, IRuntime runtime);
}
