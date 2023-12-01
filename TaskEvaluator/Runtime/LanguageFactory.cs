using System.Collections.Concurrent;
using TaskEvaluator.Language;
using TaskEvaluator.Language.Exceptions;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public sealed class LanguageFactory(IServiceProvider serviceProvider) : IDisposable {
    private readonly ConcurrentDictionary<ProgrammingLanguage, IRuntimeFactory> _runtimeFactories = new();

    public IRuntimeFactory CreateRuntimeFactory(ProgrammingLanguage language) {
        if (_runtimeFactories.TryGetValue(language, out var factory)) return factory;

        var runtimeFactory = serviceProvider.GetKeyedService<IRuntimeFactory>(language);
        if (runtimeFactory is null) throw new LanguageNotSupportedException(language);

        _runtimeFactories.TryAdd(language, runtimeFactory);

        return runtimeFactory;
    }

    public Task<IRuntime> CreateRuntime(Code code, CancellationToken token = default) {
        return CreateRuntimeFactory(code.Language).Create(code, token);
    }

    public void Dispose() {
        foreach (var runtimeFactory in _runtimeFactories.Values) {
            runtimeFactory.Dispose();
        }
    }
}
