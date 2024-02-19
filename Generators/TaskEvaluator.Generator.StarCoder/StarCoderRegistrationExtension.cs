using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Generation;
using TaskEvaluator.Modules;
namespace TaskEvaluator.Generator.StarCoder;

public static class StarCoderRegistrationExtension {
    public static TaskEvaluatorConfiguration AddStarCoder(this GeneratorConfig generator) {
        generator.Services.AddTransient<ICodeGenerator, StarCoderGenerator>();

        return generator.Config;
    }
}
