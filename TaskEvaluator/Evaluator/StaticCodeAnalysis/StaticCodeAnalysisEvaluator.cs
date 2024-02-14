using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator.StaticCodeAnalysis;

public sealed class StaticCodeAnalysisEvaluator(IRuntime runtime) : IRuntimeEvaluator {
    public async Task<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, CancellationToken token = default) {
        var runtimeResult = await runtime.StaticCodeQualityAnalysis(token);
        return new StaticCodeEvaluationResult(code.Guid, runtimeResult);
    }
}
