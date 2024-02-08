using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Language;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Modules;

public abstract class LanguageRegistration {
    public abstract ProgrammingLanguage Language { get; }

    public abstract IReg<IRuntimeFactory> RuntimeFactory { get; }
    public abstract IReg<ILanguageService> LanguageService { get; }

    public abstract IServiceCollection Register(IServiceCollection builder);

    public IServiceCollection Register<T>(IServiceCollection builder, IReg<T> x) {
        return builder.AddKeyedSingleton(
            typeof(T),
            Language,
            x.GetType().GenericTypeArguments[0]);
    }

    public interface IReg<out T>;
    private sealed class Reg<T> : IReg<T>;
    protected static IReg<T> Register<T>() => new Reg<T>();
}
