using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Avalonia.ViewModels;
using TaskEvaluator.Avalonia.Views;
namespace TaskEvaluator.Avalonia;

public partial class App(IServiceProvider serviceProvider) : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = serviceProvider.GetRequiredService<MainWindowVM>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
