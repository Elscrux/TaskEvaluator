using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Language;
using TaskEvaluator.Modules;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Specification.CSharp;

public class CSharpRegistration : LanguageRegistration {
    public override ProgrammingLanguage Language => ProgrammingLanguage.CSharp;

    public override IReg<IRuntimeFactory> RuntimeFactory => Register<CSharpDockerRuntimeFactory>();
    public override IReg<ILanguageService> LanguageService => Register<CSharpService>();

    public override IServiceCollection Register(IServiceCollection builder) => builder;
}
