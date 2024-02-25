using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TaskEvaluator.Generation;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public interface ITaskVM {
    TaskSet TaskSet { get; }
    string Name { get; }
    IObservableCollection<ICodeGenerationResultVM> GenerationResults { get; }
    [Reactive] public bool IsBusy { get; set; }
    ReactiveCommand<Unit, Task> RunCodeGeneration { get; }
    ReactiveCommand<Unit, Task> RunAll { get; }
}

public sealed class TaskVM : ViewModel, ITaskVM {
    public TaskSet TaskSet { get; }

    public string Name => TaskSet.Name;
    public IObservableCollection<ICodeGenerationResultVM> GenerationResults { get; } = new ObservableCollectionExtended<ICodeGenerationResultVM>();
    [Reactive] public bool IsBusy { get; set; }
    public ReactiveCommand<Unit, Task> RunCodeGeneration { get; }
    public ReactiveCommand<Unit, Task> RunAll { get; }

    public TaskVM(
        TaskEvaluatorVMFactory taskEvaluatorVMFactory,
        ICodeGenerationProvider codeGenerationProvider,
        TaskSet taskSet) {
        TaskSet = taskSet;

        RunCodeGeneration = ReactiveCommand.CreateRunInBackground(async () => {
            Dispatcher.UIThread.Post(() => IsBusy = true);

            var codeGeneration = codeGenerationProvider
                .GetGenerators()
                .Select(codeGenerator => {
                    var resultVM = taskEvaluatorVMFactory.CodeGenerationResultVM(taskSet, codeGenerator);
                    Dispatcher.UIThread.Post(() => GenerationResults.Add(resultVM));
                    return resultVM.Generate(taskSet.CodeGenerationTask);
                })
                .ToList();

            await Task.WhenAll(codeGeneration);

            Dispatcher.UIThread.Post(() => IsBusy = false);
        });

        RunAll = ReactiveCommand.CreateRunInBackground(async () => {
            Dispatcher.UIThread.Post(() => IsBusy = true);

            var codeGeneration = codeGenerationProvider
                .GetGenerators()
                .Select(codeGenerator => {
                    var resultVM = taskEvaluatorVMFactory.CodeGenerationResultVM(taskSet, codeGenerator);
                    Dispatcher.UIThread.Post(() => GenerationResults.Add(resultVM));
                    return resultVM.GenerateAndEvaluate();
                })
                .ToList();

            await Task.WhenAll(codeGeneration);

            Dispatcher.UIThread.Post(() => IsBusy = false);
        });
    }
}

public sealed class DesignTaskVM : ViewModel, ITaskVM {
    public TaskSet TaskSet => null!;
    public string Name { get; }
    public bool IsBusy { get; set; }
    public ReactiveCommand<Unit, Task> RunCodeGeneration { get; }
    public ReactiveCommand<Unit, Task> RunAll { get; }
    public IObservableCollection<ICodeGenerationResultVM> GenerationResults { get; } = new ObservableCollectionExtended<ICodeGenerationResultVM>();

    public DesignTaskVM(int index) {
        Name = $"Task{index}";
        IsBusy = index % 2 == 0;
        RunCodeGeneration = RunAll = ReactiveCommand.Create(() => {
            GenerationResults.AddRange(Enumerable.Range(0, 2)
                .Select(i => new DesignCodeGenerationResultVM($"Generator{i}")));

            return Task.CompletedTask;
        });
    }
}
