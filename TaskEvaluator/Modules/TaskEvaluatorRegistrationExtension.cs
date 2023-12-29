using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Docker;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Generation.GitHubCopilot;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Modules;

public static class TaskEvaluatorRegistrationExtension {
    public static IServiceCollection AddTaskEvaluator(this IServiceCollection services) {
        // AI
        services.AddTransient<ICodeGenerationProvider, InjectedCodeGenerationProvider>();

        // GitHub Copilot
        services.AddTransient<ICodeGenerator, GitHubCopilotModelApi>();
        services.AddSingleton<GitHubCopilotTokenProvider>();
        services.AddSingleton<GitHubCopilotPromptGenerator>();

        // Task
        services.AddTransient<TaskRunner>();

        // Evaluator
        services.AddTransient<IEvaluatorProvider, EvaluatorProvider>();

        // Languages
        services.AddSingleton<LanguageFactory>();

        // Docker
        services.AddSingleton<IPortPool, RandomPortPool>();
        services.AddSingleton<DockerHostFactory>();
        services.AddSingleton<DockerRuntimeFactory>();

        return services;
    }
}
