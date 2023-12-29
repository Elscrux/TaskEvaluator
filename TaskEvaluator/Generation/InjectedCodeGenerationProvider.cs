using Microsoft.Extensions.DependencyInjection;
namespace TaskEvaluator.Generation;

public class InjectedCodeGenerationProvider(IServiceProvider serviceProvider) : ICodeGenerationProvider {
    public IEnumerable<ICodeGenerator> GetGenerators() {
        return serviceProvider.GetServices<ICodeGenerator>();
    }
}
