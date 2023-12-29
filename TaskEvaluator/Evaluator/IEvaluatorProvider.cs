using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public interface IEvaluatorProvider {
    IEnumerable<IEvaluator> GetEvaluators(EvaluationModel model, IRuntime runtime);
}
