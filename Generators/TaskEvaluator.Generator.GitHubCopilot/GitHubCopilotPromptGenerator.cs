using TaskEvaluator.Generation;
using TaskEvaluator.Generator.GitHubCopilot.Model;
using TaskEvaluator.Runtime;
using Tiktoken;
namespace TaskEvaluator.Generator.GitHubCopilot;

public sealed class GitHubCopilotPromptGenerator(LanguageFactory languageFactory) {
    public static readonly Encoding Encoding = Encoding.Get(Encodings.Cl100KBase);

    public GitHubCopilotApiRequest GeneratePrompt(CodeGenerationTask task) {
        var languageService = languageFactory.GetLanguageService(task.Language);

        var prefixTokens = Encoding.Explore(task.Prefix).Count;
        var suffixTokens = Encoding.Explore(task.Suffix).Count;
        return new GitHubCopilotApiRequest {
            MaxTokens = GitHubCopilotApiRequest.TotalTokenLimit - (prefixTokens + suffixTokens),
            Prompt = task.Prefix,
            Suffix = task.Suffix,
            Extras = new GitHubCopilotApiRequest.Extra {
                Language = languageService.LanguageId,
                PromptTokens = prefixTokens,
                SuffixTokens = suffixTokens,
            }
        };
    }
}
