using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed class LocalTaskLoader(ILogger<LocalTaskLoader> logger, TaskLoadConfiguration config) : ITaskLoader {

    private const string CodeMarker = "INSERT_CODE_HERE";

    public IEnumerable<TaskSet> Load() {
        foreach (var languageDirectory in Directory.EnumerateDirectories(config.DirectoryPath)) {
            var languageFolder = Path.GetFileName(languageDirectory);
            if (!Enum.TryParse<ProgrammingLanguage>(languageFolder, out var language)) {
                logger.LogWarning("Invalid Language {LanguageFolder}", languageFolder);
                continue;
            }

            foreach (var taskDirectory in Directory.EnumerateDirectories(languageDirectory)) {
                var taskName = Path.GetFileName(taskDirectory);
                var task = LoadTask(language, taskName, taskDirectory);
                if (task is null) continue;

                yield return task;
            }
        }
    }

    private TaskSet? LoadTask(ProgrammingLanguage language, string taskName, string taskDirectory) {
        var metadata = LoadMetadata(taskDirectory);

        var codeGenerationTask = LoadCodeGenerationTask(language, taskDirectory);
        if (codeGenerationTask is null) return null;

        var evaluationModel = LoadEvaluationModel(language, taskDirectory);

        return new TaskSet(taskName, codeGenerationTask, evaluationModel, metadata);
    }

    private Metadata LoadMetadata(string taskDirectory) {
        var metadataPath = Path.Combine(taskDirectory, "metadata.json");
        if (File.Exists(metadataPath)) {
            var jsonResult = JsonSerializer.Deserialize<Metadata>(File.ReadAllText(metadataPath));
            if (jsonResult is not null) return jsonResult;
        }

        logger.LogWarning("No valid metadata.json found in {TaskDirectory}, use default metadata instead", taskDirectory);
        return Metadata.Default;
    }

    private CodeGenerationTask? LoadCodeGenerationTask(ProgrammingLanguage language, string taskDirectory) {
        if (!LoadFile(taskDirectory, "Program", out var programPath, out var code)) return null;

        var sections = code.Split(CodeMarker);
        switch (sections.Length) {
            case < 2:
                logger.LogWarning("No CodeMarker found in {ProgramPath}", programPath);
                return null;
            case > 2:
                logger.LogWarning("Multiple CodeMarkers found in {ProgramPath}", programPath);
                return null;
            default:
                return new CodeGenerationTask(sections[0], sections[1], language);
        }
    }

    private EvaluationModel LoadEvaluationModel(ProgrammingLanguage language, string taskDirectory) {
        return LoadFile(taskDirectory, "UnitTest", out _, out var unitTest)
            ? new EvaluationModel(new Code(unitTest, language))
            : new EvaluationModel();
    }

    private bool LoadFile(string directory, string pattern, [MaybeNullWhen(false)] out string filePath, [MaybeNullWhen(false)] out string code) {
        filePath = Directory.EnumerateFiles(directory)
            .FirstOrDefault(f => f.Contains(pattern, StringComparison.OrdinalIgnoreCase));

        if (filePath is null) {
            logger.LogWarning("No {Pattern} file found in {TaskDirectory}", pattern, directory);
            code = null;
            return false;
        }

        code = File.ReadAllText(filePath);
        return true;
    }
}
