using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Generation;
using TaskEvaluator.Modules;
namespace TaskEvaluator.Generator.GitHubCopilot;

public static class GitHubCopilotRegistrationExtension {
    public static TaskEvaluatorConfiguration AddGitHubCopilot(this GeneratorConfig generator) {
        generator.Services.Configure<GitHubCopilotConfiguration>(generator.Configuration.GetSection("GitHubCopilot"));
        generator.Services.AddTransient<ICodeGenerator, GitHubCopilotModelApi>();
        generator.Services.AddSingleton<GitHubCopilotTokenProvider>();
        generator.Services.AddSingleton<GitHubCopilotPromptGenerator>();

        return generator.Config;
    }
}
