using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Language;
using TaskEvaluator.Language.Exceptions;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public sealed class LanguageFactory(IServiceProvider serviceProvider) {
    public IRuntimeFactory CreateRuntimeFactory(ProgrammingLanguage language) {
        var runtimeFactory = serviceProvider.GetKeyedService<IRuntimeFactory>(language);
        if (runtimeFactory is null) throw new LanguageNotSupportedException(language);

        return runtimeFactory;
    }

    public Task<IRuntime> CreateRuntime(Code code, CancellationToken token = default) {
        return CreateRuntimeFactory(code.Language).Create(code, token);
    }

    public ILanguageService GetLanguageService(ProgrammingLanguage language) {
        var languageSpecification = serviceProvider.GetKeyedService<ILanguageService>(language);
        if (languageSpecification is null) throw new LanguageNotSupportedException(language);

        return languageSpecification;
    }
}
