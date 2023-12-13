using TaskEvaluator.Docker;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Modules;

public static class TaskEvaluatorRegistrationExtension {
    public static IServiceCollection AddTaskEvaluator(this IServiceCollection services) {
        // Task
        services.AddTransient<TaskRunner>();
        services.AddTransient<LocalTaskProvider, LocalTaskProvider>();

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
