using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Docker;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Modules;

public static class TaskEvaluatorRegistrationExtension {
    public static TaskEvaluatorConfiguration AddTaskEvaluator(this IServiceCollection services, IConfiguration configuration) {
        // AI
        services.AddTransient<ICodeGenerationProvider, InjectedCodeGenerationProvider>();

        // Task
        services.Configure<TaskLoadConfiguration>(configuration.GetSection("TaskSet"));
        services.AddTransient<TaskRunner>();
        services.AddTransient<TaskRetry>();
        services.AddTransient<ITaskLoader, LocalTaskLoader>();

        // Evaluator
        services.AddTransient<IEvaluatorProvider, EvaluatorProvider>();

        // Languages
        services.AddSingleton<LanguageFactory>();

        // Docker
        services.AddSingleton<IPortPool, RandomPortPool>();
        services.AddSingleton<DockerHostFactory>();
        services.AddSingleton<DockerRuntimeFactory>();

        return new TaskEvaluatorConfiguration(services, configuration);
    }
}
