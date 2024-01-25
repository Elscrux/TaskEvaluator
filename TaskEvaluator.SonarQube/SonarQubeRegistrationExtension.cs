using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
namespace TaskEvaluator.SonarQube;

public static class SonarQubeRegistrationExtension {
    public static IServiceCollection AddSonarQube(this IServiceCollection services) {
        services.AddTransient<SonarScannerApi>();
        services.AddTransient<SonarCubeApiFactory>();
        services.AddTransient<IStaticEvaluator>(provider => {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var sonarQubeSection = configuration.GetSection("SonarQube");
            var url = sonarQubeSection["Url"] ?? "http://localhost:9000";
            var user = sonarQubeSection["User"] ?? "admin";
            var password = sonarQubeSection["Password"] ?? "1234";

            return new SonarQubeEvaluator(
                provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
                provider.GetRequiredService<SonarScannerApi>(),
                provider.GetRequiredService<SonarCubeApiFactory>()
                    .Create(url, user, password));
        });

        return services;
    }
}
