using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Generation;

public sealed class RetryCodeGenerator<T>(
    IServiceProvider serviceProvider,
    LanguageFactory languageFactory,
    ILogger<RetryCodeGenerator<T>> logger)
    : ICodeGenerator where T : ICodeGenerator {

    private const int MaxRetries = 5;
    private readonly ICodeGenerator _codeGenerator = serviceProvider.GetRequiredService<T>();

    public async Task<CodeGenerationResult> Send(CodeGenerationTask task, CancellationToken token = default) {
        var result = await _codeGenerator.Send(task, token);
        for (var i = 0; i < MaxRetries && ShouldRetry(result); i++) {
            logger.LogWarning("Code generation failed. Retrying...");
            result = await _codeGenerator.Send(task, token);
        }

        return result;
    }

    private bool ShouldRetry(CodeGenerationResult result) {
        // Retry if the code generation failed
        if (!result.Success) return true;

        var lines = result.GeneratedPart
            .Split(Environment.NewLine)
            .Select(x => x.Trim())
            .ToList();

        // Retry if the code is empty
        if (lines.Count == 0) return true;

        // Retry if the code is a single line comment
        var firstLine = lines[0];
        if (string.IsNullOrWhiteSpace(firstLine)) return true;

        var languageService = languageFactory.GetLanguageService(result.Code.Language);
        var firstLineIsComment = firstLine.StartsWith(languageService.LineCommentSymbol, StringComparison.OrdinalIgnoreCase);

        // Retry if the code is a single line comment
        if (lines.Count == 1 && firstLineIsComment) return true;

        // Retry if the code is just a to-do comment
        if (firstLineIsComment && lines[0].Contains("Write your code here", StringComparison.OrdinalIgnoreCase)) return true;

        return false;
    }
}
