using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
namespace TaskEvaluator.SonarQube;

public static class SonarQubeRegistrationExtension {
    public static IServiceCollection AddSonarQube(this IServiceCollection services, string sonarQubeUrl, string sonarQubeUserName, string sonarQubePassword) {
        services.AddTransient<SonarScannerApi>();
        services.AddTransient<SonarCubeApiFactory>();
        services.AddTransient<IStaticEvaluator>(provider => new SonarQubeEvaluator(
            provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
            provider.GetRequiredService<SonarScannerApi>(),
            provider.GetRequiredService<SonarCubeApiFactory>()
                .Create(sonarQubeUrl, sonarQubeUserName, sonarQubePassword)));

        return services;
    }
}
