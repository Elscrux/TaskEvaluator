using TaskEvaluator.Tasks;
namespace TaskEvaluator.Sinks;

public interface IFinalResultSink {
    void Send(FinalResult finalResult);
}
