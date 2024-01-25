using TaskEvaluator.Generation.GitHubCopilot.Model;
using TaskEvaluator.Language;
using Tiktoken;
namespace TaskEvaluator.Generation.GitHubCopilot;

public sealed class GitHubCopilotPromptGenerator {
    public static readonly Encoding Encoding = Encoding.Get(Encodings.Cl100KBase);

    public GitHubCopilotApiRequest GeneratePrompt(CodeGenerationTask task) {
        return new GitHubCopilotApiRequest {
            Prompt = task.Prefix,
            Suffix = task.Suffix,
            Extras = new GitHubCopilotApiRequest.Extra {
                Language = task.Language switch {
                    ProgrammingLanguage.CSharp => "csharp",
                    _ => throw new NotImplementedException()
                },
                PromptTokens = Encoding.Explore(task.Prefix).Count,
                SuffixTokens = Encoding.Explore(task.Suffix).Count,
            }
        };
    }
}
