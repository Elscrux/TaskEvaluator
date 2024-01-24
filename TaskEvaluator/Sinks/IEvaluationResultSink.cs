using TaskEvaluator.Evaluator;
namespace TaskEvaluator.Sinks;

public interface IEvaluationResultSink {
    void Send(IEvaluationResult evaluationResult);
}
