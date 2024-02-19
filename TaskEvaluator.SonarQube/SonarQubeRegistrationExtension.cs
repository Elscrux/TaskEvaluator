using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Modules;
using TaskEvaluator.Runtime;
using TaskEvaluator.SonarQube.Scanner;
namespace TaskEvaluator.SonarQube;

public static class SonarQubeRegistrationExtension {
    public static TaskEvaluatorConfiguration AddSonarQube(this EvaluatorConfig evaluator) {
        // Sonar Scanner
        evaluator.Services.AddTransient<ISonarScannerApiFactory, SonarScannerApiFactory>();
        evaluator.Services.AddTransient<BatchSonarScannerApi>();
        evaluator.Services.AddTransient<DotNetSonarScannerApi>();

        // Sonar Qube
        evaluator.Services.Configure<SonarQubeConfiguration>(evaluator.Configuration.GetSection("SonarQube"));
        evaluator.Services.AddTransient<SonarQubeApiFactory>();
        evaluator.Services.AddTransient<Task<IStaticEvaluator?>>(async provider => {
            var sonarQubeApi = await GetSonarQubeApi(provider);
            if (sonarQubeApi is null) return null;

            return new SonarQubeEvaluator(
                provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
                provider.GetRequiredService<ISonarScannerApiFactory>(),
                sonarQubeApi,
                provider.GetRequiredService<LanguageFactory>());
        });
        evaluator.Services.AddTransient<Task<SonarQubeEvaluator?>>(async provider => {
            var sonarQubeApi = await GetSonarQubeApi(provider);
            if (sonarQubeApi is null) return null;

            return new SonarQubeEvaluator(
                provider.GetRequiredService<ILogger<SonarQubeEvaluator>>(),
                provider.GetRequiredService<ISonarScannerApiFactory>(),
                sonarQubeApi,
                provider.GetRequiredService<LanguageFactory>());
        });

        return evaluator.Config;

        Task<SonarQubeApi?> GetSonarQubeApi(IServiceProvider provider) {
            var sonarQubeOptions = provider.GetRequiredService<IOptions<SonarQubeConfiguration>>().Value;
            return provider.GetRequiredService<SonarQubeApiFactory>().Create(sonarQubeOptions);
        }
    }
}
