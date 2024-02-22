using TaskEvaluator.Generation;
using TaskEvaluator.Generator.GitHubCopilot.Model;
using TaskEvaluator.Language;
using TaskEvaluator.Runtime;
using Tiktoken;
namespace TaskEvaluator.Generator.GitHubCopilot;

public sealed class GitHubCopilotPromptGenerator(LanguageFactory languageFactory) {
    public static readonly Encoding Encoding = Encoding.Get(Encodings.Cl100KBase);

    public GitHubCopilotApiRequest GeneratePrompt(CodeGenerationTask task) {
        var languageService = languageFactory.GetLanguageService(task.Language);

        return new GitHubCopilotApiRequest {
            Prompt = task.Prefix,
            Suffix = task.Suffix,
            Extras = new GitHubCopilotApiRequest.Extra {
                Language = languageService.LanguageId,
                PromptTokens = Encoding.Explore(task.Prefix).Count,
                SuffixTokens = Encoding.Explore(task.Suffix).Count,
            }
        };
    }
}
