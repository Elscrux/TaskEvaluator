using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.ComponentModel.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using TaskEvaluator.Avalonia.ViewModels;
using TaskEvaluator.Generator.GitHubCopilot;
using TaskEvaluator.Generator.Tabby;
using TaskEvaluator.Modules;
using TaskEvaluator.Sink.PostgreSQL;
using TaskEvaluator.SonarQube;
using TaskEvaluator.Specification.CSharp;
using TaskEvaluator.Tasks;

namespace TaskEvaluator.Avalonia;

public static class Program {
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) {
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddLogging();
        builder.Services.AddHttpClient();

        builder.Configuration.AddUserSecrets<TaskRunner>();

        builder.Services.AddTaskEvaluator(builder.Configuration)
            .Language.Add<CSharpRegistration>()
            .Generator.AddGitHubCopilot()
            .Generator.AddTabby()
            .Evaluator.AddSonarQube()
            .Sink.AddLogger()
            .Sink.AddPostgreSQL();

        builder.Services.AddTransient<MainWindowVM>();
        builder.Services.AddTransient<RunnerVM>();
        builder.Services.AddTransient<TaskEvaluatorVMFactory>();

        var host = builder.Build();

        BuildAvaloniaApp(host.Services)
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp() => BuildAvaloniaApp(() => new App(new ServiceContainer()));
    public static AppBuilder BuildAvaloniaApp(IServiceProvider serviceProvider) => BuildAvaloniaApp(() => new App(serviceProvider));

    public static AppBuilder BuildAvaloniaApp(Func<App> appFactory) {
        IconProvider.Current
            .Register<FontAwesomeIconProvider>();

        return AppBuilder.Configure(appFactory)
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }
}
