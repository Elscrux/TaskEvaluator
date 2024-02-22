using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Sinks;
using TaskEvaluator.Sinks.Logger;
namespace TaskEvaluator.Modules;

public sealed class SinkConfig(TaskEvaluatorConfiguration config, IServiceCollection services, IConfiguration configuration) {
    public TaskEvaluatorConfiguration Config { get; set; } = config;
    public IServiceCollection Services { get; set; } = services;
    public IConfiguration Configuration { get; set; } = configuration;

    public TaskEvaluatorConfiguration AddLogger() {
        Services.AddTransient<IFinalResultSink, LoggerFinalResultSink>();
        return Config;
    }
}