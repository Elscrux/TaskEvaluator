using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace TaskEvaluator.Modules;

public sealed class TaskEvaluatorConfiguration {
    public SinkConfig Sink { get; set; }
    public LanguageConfig Language { get; set; }
    public EvaluatorConfig Evaluator { get; set; }

    public TaskEvaluatorConfiguration(IServiceCollection services, IConfiguration configuration) {
        Sink = new SinkConfig(this, services, configuration);
        Language = new LanguageConfig(this, services, configuration);
        Evaluator = new EvaluatorConfig(this, services, configuration);
    }
}
