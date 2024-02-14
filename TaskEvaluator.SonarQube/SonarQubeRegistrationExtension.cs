using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Runtime;
using TaskEvaluator.SonarQube.Scanner;
namespace TaskEvaluator.SonarQube;

public static class SonarQubeRegistrationExtension {
    public static IServiceCollection AddSonarQube(this IServiceCollection services, IConfiguration configuration) {
        // Sonar Scanner
        services.AddTransient<ISonarScannerApiFactory, SonarScannerApiFactory>();
        services.AddTransient<BatchSonarScannerApi>();
        services.AddTransient<DotNetSonarScannerApi>();

        // Sonar Qube
        services.Configure<SonarQubeConfiguration>(configuration.GetSection("SonarQube"));
        services.AddTransient<SonarQubeApiFactory>();
        services.AddTransient<Task<IStaticEvaluator?>>(async provider => {
            var sonarQubeApi = await GetSonarQubeApi(provider);
            if (sonarQubeApi is null) return null;

            return new SonarQubeEvaluator(
                provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
                provider.GetRequiredService<ISonarScannerApiFactory>(),
                sonarQubeApi,
                provider.GetRequiredService<LanguageFactory>());
        });
        services.AddTransient<Task<SonarQubeEvaluator?>>(async provider => {
            var sonarQubeApi = await GetSonarQubeApi(provider);
            if (sonarQubeApi is null) return null;

            return new SonarQubeEvaluator(
                provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
                provider.GetRequiredService<ISonarScannerApiFactory>(),
                sonarQubeApi,
                provider.GetRequiredService<LanguageFactory>());
        });

        return services;

        Task<SonarQubeApi?> GetSonarQubeApi(IServiceProvider provider) {
            var sonarQubeOptions = provider.GetRequiredService<IOptions<SonarQubeConfiguration>>().Value;
            return provider.GetRequiredService<SonarQubeApiFactory>().Create(sonarQubeOptions);
        }
    }
}
