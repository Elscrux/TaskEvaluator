using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Language;
namespace TaskEvaluator.SonarQube.Scanner;

public sealed class SonarScannerApiFactory(IServiceProvider provider) : ISonarScannerApiFactory {
    public ISonarScannerApi Create(ProgrammingLanguage language) {
        return language switch {
            ProgrammingLanguage.CSharp => provider.GetRequiredService<DotNetSonarScannerApi>(),
            _ => provider.GetRequiredService<BatchSonarScannerApi>()
        };
    }
}
