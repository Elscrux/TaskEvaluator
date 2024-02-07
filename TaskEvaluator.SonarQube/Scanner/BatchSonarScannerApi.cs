using System.Diagnostics;
using System.IO.Compression;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.SonarQube.Scanner;

public sealed class BatchSonarScannerApi(
    IHttpClientFactory httpClientFactory,
    ILogger<BatchSonarScannerApi> logger)
    : ISonarScannerApi {

    private static readonly string OsTag = OperatingSystem.IsWindows() ? "windows" : "linux";
    private static readonly string DownloadUrl = $"https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-5.0.1.3006-{OsTag}.zip";
    private const string DownloadPath = "sonar-scanner.zip";
    private const string ExtractPath = "sonar-scanner";
    private static readonly string BatPath = Path.Combine(ExtractPath, $"sonar-scanner-5.0.1.3006-{OsTag}", "bin", "sonar-scanner.bat");

    private async Task<bool> Install() {
        if (!File.Exists(DownloadPath)) {
            using var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(DownloadUrl);
            await using var stream = await httpClient.GetStreamAsync(DownloadUrl);
            await using var fileStream = File.Create(DownloadPath);
            await stream.CopyToAsync(fileStream);
        }

        ZipFile.ExtractToDirectory(DownloadPath, ExtractPath, true);

        return true;
    }

    public async Task<bool> Run(string workingDirectory, Code code, string url, string token, string projectKey, CancellationToken cancellationToken = default) {
        if (!await Install()) {
            logger.LogError("Failed to install sonar-scanner");
            return false;
        }

        if (!File.Exists(BatPath)) {
            logger.LogError("Failed to find sonar-scanner at {BatPath}", BatPath);
            return false;
        }

        var process = Process.Start(
            new ProcessStartInfo(Path.GetFullPath(BatPath), [
                $"-D sonar.host.url={url}",
                $"-D sonar.token={token}",
                $"-D sonar.projectKey={projectKey}"
            ]) {
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            }
        );

        if (process == null) {
            logger.LogError("Failed to start sonar-scanner at {WorkingDirectory}", workingDirectory);
            return false;
        }

        // read the output
        var output = await process.StandardOutput.ReadToEndAsync(cancellationToken);
        var error = await process.StandardError.ReadToEndAsync(cancellationToken);

        // wait for the process to exit
        await process.WaitForExitAsync(cancellationToken);

        // log the output
        logger.LogInformation("Output: {Output}", output);
        if (!string.IsNullOrWhiteSpace(error)) {
            logger.LogInformation("Error: {Error}", error);
        }

        return true;
    }
}
