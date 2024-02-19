using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Modules;
using TaskEvaluator.Sinks;
namespace TaskEvaluator.Sink.PostgreSQL;

public static class PostgresRegistrationExtension {
    public static TaskEvaluatorConfiguration AddPostgreSQL(this SinkConfig sink) {
        sink.Services.Configure<PostgresSinkConfiguration>(sink.Configuration.GetSection("Database"));
        sink.Services.AddTransient<IEvaluationResultSink, PostgresEvaluationResultSink>();

        return sink.Config;
    }
}
