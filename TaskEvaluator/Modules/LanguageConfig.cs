using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace TaskEvaluator.Modules;

public sealed class LanguageConfig(TaskEvaluatorConfiguration config, IServiceCollection services, IConfiguration configuration) {
    public TaskEvaluatorConfiguration Config { get; set; } = config;
    public IServiceCollection Services { get; set; } = services;
    public IConfiguration Configuration { get; set; } = configuration;

    public TaskEvaluatorConfiguration Add<T>() where T : LanguageRegistration, new() {
        var registration = new T();
        registration.Register(Services, registration.RuntimeFactory);
        registration.Register(Services, registration.LanguageService);

        registration.Register(Services);

        return Config;
    }
}
