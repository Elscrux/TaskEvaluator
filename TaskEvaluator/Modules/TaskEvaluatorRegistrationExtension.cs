using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskEvaluator.Docker;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Generation.GitHubCopilot;
using TaskEvaluator.Runtime;
using TaskEvaluator.Sinks;
using TaskEvaluator.Sinks.Database;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Modules;

public static class TaskEvaluatorRegistrationExtension {
    public static IServiceCollection AddTaskEvaluator(this IServiceCollection services, IConfiguration configuration) {
        // AI
        services.AddTransient<ICodeGenerationProvider, InjectedCodeGenerationProvider>();

        // GitHub Copilot
        services.Configure<IOptions<GitHubCopilotConfiguration>>(configuration.GetSection("GitHubCopilot"));
        services.AddTransient<ICodeGenerator, GitHubCopilotModelApi>();
        services.AddSingleton<GitHubCopilotTokenProvider>();
        services.AddSingleton<GitHubCopilotPromptGenerator>();

        // Task
        services.Configure<IOptions<TaskLoadConfiguration>>(configuration.GetSection("TaskSet"));
        services.AddTransient<TaskRunner>();
        services.AddTransient<ITaskLoader, LocalTaskLoader>();

        // Evaluator
        services.AddTransient<IEvaluatorProvider, EvaluatorProvider>();

        // Languages
        services.AddSingleton<LanguageFactory>();

        // Docker
        services.AddSingleton<IPortPool, RandomPortPool>();
        services.AddSingleton<DockerHostFactory>();
        services.AddSingleton<DockerRuntimeFactory>();

        // Sink
        services.Configure<IOptions<EvaluationResultDatabaseSinkConfiguration>>(configuration.GetSection("Database"));
        services.AddSingleton<IEvaluationResultSink, EvaluationResultDatabaseSink>();

        return services;
    }
}
