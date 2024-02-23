using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator.SyntaxValidation;

public sealed class SyntaxValidationEvaluator(IRuntime runtime) : IRuntimeEvaluator {
    public async Task<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, CancellationToken token = default) {
        var runtimeResult = await runtime.SyntaxValidation(token);

        return new SyntaxValidationResult(code.Guid, runtimeResult);
    }
}
