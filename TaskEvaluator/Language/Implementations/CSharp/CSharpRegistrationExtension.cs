using TaskEvaluator.Runtime;
namespace TaskEvaluator.Language.Implementations.CSharp;

public static class CSharpRegistrationExtension {
    public static IServiceCollection AddCSharp(this IServiceCollection services) {
        services.AddKeyedTransient<IRuntimeFactory, CSharpMethodRuntimeFactory>(ProgrammingLanguage.CSharp);

        return services;
    }
}
