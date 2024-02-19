using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Sinks;
namespace TaskEvaluator.Sink.PostgreSQL;

public static class PostgresRegistrationExtension {
    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, IConfiguration configuration) {
        
        services.Configure<PostgresSinkConfiguration>(configuration.GetSection("Database"));
        services.AddTransient<IEvaluationResultSink, PostgresEvaluationResultSink>();

        return services;
    }
}
