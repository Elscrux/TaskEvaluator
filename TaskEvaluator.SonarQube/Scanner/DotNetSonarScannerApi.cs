using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.SonarQube.Scanner;

public sealed class DotNetSonarScannerApi(ILogger<DotNetSonarScannerApi> logger) : ISonarScannerApi {
    public ProcessStartInfo GetShell(string command) => OperatingSystem.IsWindows()
        ? new ProcessStartInfo("cmd", $"/c {command}")
        : new ProcessStartInfo("/bin/bash", $"-c \"{command}\"");

    private async Task<bool> Install() {
        const string tool = "dotnet tool install --global dotnet-sonarscanner";

        var shell = GetShell(tool);
        Console.WriteLine("Start using " + shell.FileName + " | " + shell.Arguments);

        shell.RedirectStandardOutput = true;
        shell.RedirectStandardError = true;
        shell.UseShellExecute = false;

        var process = Process.Start(shell);

        if (process == null) {
            logger.LogError("Failed to start dotnet-sonarscanner installation");
            return false;
        }

        var standardOutput = await process.StandardOutput.ReadToEndAsync();
        var errorOutput = await process.StandardError.ReadToEndAsync();

        logger.LogInformation("dotnet-sonarscanner output: {StandardOutput}", standardOutput + errorOutput);

        await process.WaitForExitAsync();

        switch (process.ExitCode) {
            case 0:
                logger.LogInformation("dotnet-sonarscanner installed");
                return true;
            case 1:
                logger.LogInformation("dotnet-sonarscanner is already installed");
                return true;
            default:
                logger.LogError("dotnet-sonarscanner failed with exit code {ExitCode}: {Message}", process.ExitCode, errorOutput);
                return false;
        }
    }

    public async Task<bool> Run(string workingDirectory, Code code, string url, string token, string projectKey, CancellationToken cancellationToken = default) {
        if (Array.Exists(workingDirectory.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar), dir => dir == "bin")) {
            logger.LogError("Working directory {Directory} cannot be in bin directory as sonar-scanner ignores this", workingDirectory);
            return false;
        }

        if (!await Install()) {
            logger.LogError("Failed to install dotnet-sonarscanner");
            return false;
        }

        var start = $"dotnet sonarscanner begin /k:\"{projectKey}\" /d:sonar.host.url=\"{url}\"  /d:sonar.token=\"{token}\"";
        const string build = "dotnet build";
        var final = $"dotnet sonarscanner end /d:sonar.token=\"{token}\"";

        var shell = GetShell($"{start} && {build} && {final}");

        shell.WorkingDirectory = workingDirectory;
        shell.RedirectStandardOutput = true;
        shell.RedirectStandardError = true;
        shell.UseShellExecute = false;

        var process = Process.Start(shell);
        if (process == null) {
            logger.LogError("Failed to start sonar-scanner");
            return false;
        }

        var standardOutput = await process.StandardOutput.ReadToEndAsync(cancellationToken);
        var errorOutput = await process.StandardError.ReadToEndAsync(cancellationToken);

        logger.LogInformation("sonar-scanner output: {StandardOutput}", standardOutput);
        logger.LogInformation("sonar-scanner error: {ErrorOutput}", errorOutput);

        await process.WaitForExitAsync(cancellationToken);

        switch (process.ExitCode) {
            case 0:
                return true;
            default:
                logger.LogError("Sonar-scanner failed with exit code {ExitCode}", process.ExitCode);
                return false;
        }
    }
}
