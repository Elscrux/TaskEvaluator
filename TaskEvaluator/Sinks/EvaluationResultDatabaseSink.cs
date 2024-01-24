using TaskEvaluator.Evaluator;
namespace TaskEvaluator.Sinks;

public sealed class EvaluationResultDatabaseSink : IEvaluationResultSink {
    public EvaluationResultDatabaseSink(
        string connectionString) {
        
    }

    public void Send(IEvaluationResult evaluationResult) {
        
    }
}
