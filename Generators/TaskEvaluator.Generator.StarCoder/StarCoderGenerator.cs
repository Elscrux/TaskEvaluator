using System.Diagnostics;
using TaskEvaluator.Generation;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Generator.StarCoder;

public class StarCoderGenerator : ICodeGenerator {
    public async Task<CodeGenerationResult> Send(CodeGenerationTask task, CancellationToken token = default) {
        await InstallRequirements(token);

        // Run the python script
        var processStartInfo = new ProcessStartInfo("python", "./Generators/TaskEvaluator.Generator.StarCoder/star_coder.py") {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        var process = Process.Start(processStartInfo);

        if (process == null) {
            throw new InvalidOperationException("Could not start the process.");
        }

        await process.WaitForExitAsync(token);

        var output = await process.StandardOutput.ReadToEndAsync(token);
        var error = await process.StandardError.ReadToEndAsync(token);

        if (process.ExitCode != 0) {
            throw new InvalidOperationException($"Process exited with code {process.ExitCode}. Error: {error}");
        }

        return new CodeGenerationResult(true, new Code(output, task.Language));
    }

    private async Task InstallRequirements(CancellationToken token = default) {
        // Install requirements
        var processStartInfo = new ProcessStartInfo("pip", "install -r ./Generators/TaskEvaluator.Generator.StarCoder/requirements.txt") {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        var process = Process.Start(processStartInfo);

        if (process == null) {
            throw new InvalidOperationException("Could not start the process.");
        }

        await process.WaitForExitAsync(token);

        var output = await process.StandardOutput.ReadToEndAsync(token);
        var error = await process.StandardError.ReadToEndAsync(token);

        if (process.ExitCode != 0) {
            throw new InvalidOperationException($"Process exited with code {process.ExitCode}. Error: {error}");
        }

        Console.WriteLine(output);
    }
}
