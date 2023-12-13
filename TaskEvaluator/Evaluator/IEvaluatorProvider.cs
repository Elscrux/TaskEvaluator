using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public interface IEvaluatorProvider {
    IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model, IRuntime runtime);
}
