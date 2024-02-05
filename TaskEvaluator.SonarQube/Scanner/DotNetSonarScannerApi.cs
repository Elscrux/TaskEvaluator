using TaskEvaluator.Tasks;
namespace TaskEvaluator.SonarQube.Scanner;

public sealed class DotNetSonarScannerApi : ISonarScannerApi {
    public Task<bool> Run(Code code, string url, string token, string projectKey, CancellationToken cancellationToken = default) {
        return Task.FromResult(false);
    }
}
