using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
using TaskEvaluator.SonarQube.Scanner;
namespace TaskEvaluator.SonarQube;

public static class SonarQubeRegistrationExtension {
    public static IServiceCollection AddSonarQube(this IServiceCollection services) {
        // Sonar Scanner
        services.AddTransient<ISonarScannerApiFactory, SonarScannerApiFactory>();
        services.AddTransient<BatchSonarScannerApi>();
        services.AddTransient<DotNetSonarScannerApi>();

        // Sonar Qube
        services.AddTransient<SonarQubeApiFactory>();
        services.AddTransient<Task<IStaticEvaluator?>>(async provider => {
            var sonarQubeApi = await GetSonarQubeApi(provider);
            if (sonarQubeApi is null) return null;

            return new SonarQubeEvaluator(
                provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
                provider.GetRequiredService<ISonarScannerApiFactory>(),
                sonarQubeApi);
        });
        services.AddTransient<Task<SonarQubeEvaluator?>>(async provider => {
            var sonarQubeApi = await GetSonarQubeApi(provider);
            if (sonarQubeApi is null) return null;

            return new SonarQubeEvaluator(
                provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
                provider.GetRequiredService<ISonarScannerApiFactory>(),
                sonarQubeApi);
        });

        return services;

        Task<SonarQubeApi?> GetSonarQubeApi(IServiceProvider provider) {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var sonarQubeSection = configuration.GetSection("SonarQube");
            var url = sonarQubeSection["Url"] ?? "http://localhost:9000";
            var user = sonarQubeSection["User"] ?? "admin";
            var password = sonarQubeSection["Password"] ?? "1234";

            return provider.GetRequiredService<SonarQubeApiFactory>()
                .Create(url, user, password);
        }
    }
}
