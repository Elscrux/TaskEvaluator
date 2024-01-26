using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        services.AddSingleton<GitHubCopilotConfiguration>(provider => {
            var config = provider.GetRequiredService<IConfiguration>();
            var copilotSection = config.GetSection("GitHubCopilot");
            return new GitHubCopilotConfiguration(
                copilotSection["TokenUrl"] ?? throw new InvalidDataException("GitHub Copilot: TokenUrl is not set."),
                copilotSection["BearerToken"] ?? throw new InvalidDataException("GitHub Copilot: BearerToken is not set."),
                copilotSection["CompletionsUrl"] ?? throw new InvalidDataException("GitHub Copilot: CompletionsUrl is not set."),
                copilotSection["UserAgent"] ?? throw new InvalidDataException("GitHub Copilot: UserAgent is not set."),
                copilotSection["UserAgentVersion"] ?? throw new InvalidDataException("GitHub Copilot: UserAgentVersion is not set."),
                copilotSection["EditorVersion"] ?? throw new InvalidDataException("GitHub Copilot: EditorVersion is not set."),
                copilotSection["EditorPluginVersion"] ?? throw new InvalidDataException("GitHub Copilot: EditorPluginVersion is not set."),
                copilotSection["Openai-Organization"] ?? throw new InvalidDataException("GitHub Copilot: Openai-Organization is not set."),
                copilotSection["Openai-Intent"] ?? throw new InvalidDataException("GitHub Copilot: Openai-Intent is not set.")
            );
        });

        // Task
        services.AddTransient<TaskRunner>();
        services.AddTransient<ITaskLoader>(provider => {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var taskSetSection = configuration.GetSection("TaskSet");
            var directoryPath = taskSetSection["DirectoryPath"] ?? string.Empty;

            var logger = provider.GetRequiredService<ILogger<LocalTaskLoader>>();
            return new LocalTaskLoader(logger, directoryPath);
        });

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
