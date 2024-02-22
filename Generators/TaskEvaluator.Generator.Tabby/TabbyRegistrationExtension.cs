using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Generation;
using TaskEvaluator.Modules;
namespace TaskEvaluator.Generator.Tabby;

public static class TabbyRegistrationExtension {
    public static TaskEvaluatorConfiguration AddTabby(this GeneratorConfig generator) {
        generator.Services.Configure<TabbyConfiguration>(generator.Configuration.GetSection("Tabby"));
        generator.Services.AddTransient<ICodeGenerator, TabbyCodeGenerator>();

        return generator.Config;
    }
}
