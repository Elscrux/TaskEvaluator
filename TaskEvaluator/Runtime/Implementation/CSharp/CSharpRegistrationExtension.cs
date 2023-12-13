using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Language;
namespace TaskEvaluator.Runtime.Implementation.CSharp;

public static class CSharpRegistrationExtension {
    public static IServiceCollection AddCSharp(this IServiceCollection services) {
        services.AddKeyedSingleton<IRuntimeFactory, CSharpDockerRuntimeFactory>(ProgrammingLanguage.CSharp);

        return services;
    }
}
