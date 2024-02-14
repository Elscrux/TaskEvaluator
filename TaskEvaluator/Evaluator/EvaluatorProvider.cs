using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator;

public sealed class EvaluatorProvider(IServiceProvider serviceProvider) : IEvaluatorProvider {
    public async IAsyncEnumerable<IEvaluator> GetEvaluators(EvaluationModel model, IRuntime runtime) {
        if (model.UnitTests is not null) yield return new UnitTestEvaluator(runtime);

        yield return new StaticCodeAnalysisEvaluator(runtime);
    }
}
