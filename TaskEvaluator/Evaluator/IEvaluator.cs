using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public interface IEvaluator {
    IAsyncEnumerable<IEvaluationResult> Evaluate(TaskEvaluationModel model, IRuntime runtime, CancellationToken token = default);
}
