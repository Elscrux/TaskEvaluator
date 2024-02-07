using TaskEvaluator.Tasks;
namespace TaskEvaluator.SonarQube.Scanner;

public interface ISonarScannerApi {
    Task<bool> Run(string workingDirectory, Code code, string url, string token, string projectKey, CancellationToken cancellationToken = default);
}
