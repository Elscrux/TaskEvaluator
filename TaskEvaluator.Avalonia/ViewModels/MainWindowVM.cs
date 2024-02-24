namespace TaskEvaluator.Avalonia.ViewModels;

public sealed class MainWindowVM(RunnerVM runnerVM) : ViewModel {
    public RunnerVM RunnerVM { get; } = runnerVM;
}
