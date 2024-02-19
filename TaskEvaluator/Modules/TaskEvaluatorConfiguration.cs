using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace TaskEvaluator.Modules;

public sealed class TaskEvaluatorConfiguration {
    public LanguageConfig Language { get; set; }
    public EvaluatorConfig Evaluator { get; set; }
    public GeneratorConfig Generator { get; set; }
    public SinkConfig Sink { get; set; }

    public TaskEvaluatorConfiguration(IServiceCollection services, IConfiguration configuration) {
        Language = new LanguageConfig(this, services, configuration);
        Generator = new GeneratorConfig(this, services, configuration);
        Evaluator = new EvaluatorConfig(this, services, configuration);
        Sink = new SinkConfig(this, services, configuration);
    }
}
