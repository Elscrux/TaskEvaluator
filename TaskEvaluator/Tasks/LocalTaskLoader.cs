using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Language;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Tasks;

public sealed class LocalTaskLoader(
    LanguageFactory languageFactory,
    ILogger<LocalTaskLoader> logger,
    IOptions<TaskLoadConfiguration> config) : ITaskLoader {

    private string CodeMarker(ILanguageService languageService) => languageService.LineCommentSymbol + "INSERT_CODE_HERE";

    public IEnumerable<TaskSet> Load() {
        foreach (var languageDirectory in Directory.EnumerateDirectories(config.Value.DirectoryPath)) {
            var languageFolder = Path.GetFileName(languageDirectory);
            if (!Enum.TryParse<ProgrammingLanguage>(languageFolder, out var language)) {
                logger.LogWarning("Invalid Language {LanguageFolder}", languageFolder);
                continue;
            }

            var languageService = languageFactory.GetLanguageService(language);
            foreach (var taskDirectory in Directory.EnumerateDirectories(languageDirectory)) {
                var taskName = Path.GetFileName(taskDirectory);
                var task = LoadTask(languageService, taskName, taskDirectory);
                if (task is null) continue;

                yield return task;
            }
        }
    }

    private TaskSet? LoadTask(ILanguageService languageService, string taskName, string taskDirectory) {
        var metadata = LoadMetadata(taskDirectory);

        var codeGenerationTask = LoadCodeGenerationTask(metadata, languageService, taskDirectory);
        if (codeGenerationTask is null) return null;

        var evaluationModel = LoadEvaluationModel(languageService, taskDirectory);

        return new TaskSet(taskName, codeGenerationTask, evaluationModel, metadata);
    }

    private TaskMetadata LoadMetadata(string taskDirectory) {
        var metadataPath = Path.Combine(taskDirectory, "metadata.json");
        if (File.Exists(metadataPath)) {
            var jsonResult = JsonSerializer.Deserialize<TaskMetadata>(File.ReadAllText(metadataPath));
            if (jsonResult is not null) return jsonResult;
        }

        logger.LogWarning("No valid metadata.json found in {TaskDirectory}, use default metadata instead", taskDirectory);
        var newMetadata = TaskMetadata.Default;
        File.WriteAllText(metadataPath, JsonSerializer.Serialize(newMetadata));
        return newMetadata;
    }

    private CodeGenerationTask? LoadCodeGenerationTask(TaskMetadata metadata, ILanguageService languageService, string taskDirectory) {
        if (!LoadFile(taskDirectory, "Program", out var programPath, out var code)) return null;

        var sections = code.Split(CodeMarker(languageService));
        switch (sections.Length) {
            case < 2:
                logger.LogWarning("No CodeMarker found in {ProgramPath}", programPath);
                return null;
            case > 2:
                logger.LogWarning("Multiple CodeMarkers found in {ProgramPath}", programPath);
                return null;
            default:
                return new CodeGenerationTask(metadata.TaskId, sections[0], sections[1], languageService.Language);
        }
    }

    private EvaluationModel LoadEvaluationModel(ILanguageService languageService, string taskDirectory) {
        return LoadFile(taskDirectory, "UnitTest", out _, out var unitTest)
            ? new EvaluationModel(new Code(unitTest, languageService.Language))
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
