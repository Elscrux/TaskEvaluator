using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace TaskEvaluator.Modules;

public sealed class GeneratorConfig(TaskEvaluatorConfiguration config, IServiceCollection services, IConfiguration configuration) {
    public TaskEvaluatorConfiguration Config { get; set; } = config;
    public IServiceCollection Services { get; set; } = services;
    public IConfiguration Configuration { get; set; } = configuration;
}
