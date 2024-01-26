using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator;

public sealed class EvaluatorProvider(IServiceProvider serviceProvider) : IEvaluatorProvider {
    public async IAsyncEnumerable<IEvaluator> GetEvaluators(EvaluationModel model, IRuntime runtime) {
        if (model.UnitTests is not null) yield return new UnitTestEvaluator(runtime);

        foreach (var staticEvaluator in serviceProvider.GetServices<IStaticEvaluator>()) {
            yield return staticEvaluator;
        }

        foreach (var staticEvaluatorTask in serviceProvider.GetServices<Task<IStaticEvaluator?>>()) {
            var staticEvaluator = await staticEvaluatorTask;
            if (staticEvaluator is not null) yield return staticEvaluator;
        }
    }
}
