using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using TaskEvaluator.Generation;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public interface ITaskVM {
    TaskSet TaskSet { get; }
    string Name { get; }

    IObservableCollection<ICodeGenerationResultVM> GenerationResults { get; }
    IObservable<TaskState> CurrentState { get; }
    ReactiveCommand<Unit, Task> RunCodeGeneration { get; }
    ReactiveCommand<Unit, Task> RunAll { get; }
}

public sealed class TaskVM : ViewModel, ITaskVM {
    public TaskSet TaskSet { get; }

    public string Name => TaskSet.Name;
    public IObservableCollection<ICodeGenerationResultVM> GenerationResults { get; } = new ObservableCollectionExtended<ICodeGenerationResultVM>();
    public IObservable<TaskState> CurrentState { get; }
    public ReactiveCommand<Unit, Task> RunCodeGeneration { get; }
    public ReactiveCommand<Unit, Task> RunAll { get; }

    public TaskVM(
        TaskEvaluatorVMFactory taskEvaluatorVMFactory,
        ICodeGenerationProvider codeGenerationProvider,
        TaskSet taskSet) {
        TaskSet = taskSet;

        CurrentState = GenerationResults.ObserveCollectionChanges()
            .Select(_ => {
                if (GenerationResults.Count == 0) return Observable.Return(TaskState.NotStarted);

                return GenerationResults
                    .Select(x => x.EvaluationCompleted)
                    .CombineLatest()
                    .Select(x => {
                        var taskState = x.Merge();
                        if (taskState == TaskState.NotStarted) return TaskState.Running;

                        return taskState;
                    });
            })
            .Switch()
            .StartWith(TaskState.NotStarted);

        RunCodeGeneration = ReactiveCommand.CreateRunInBackground(async () => {
            var codeGeneration = codeGenerationProvider
                .GetGenerators()
                .Select(codeGenerator => {
                    var resultVM = taskEvaluatorVMFactory.CodeGenerationResultVM(taskSet, codeGenerator);
                    Dispatcher.UIThread.Post(() => GenerationResults.Add(resultVM));
                    return resultVM.Generate(taskSet.CodeGenerationTask);
                })
                .ToList();

            await Task.WhenAll(codeGeneration);
        });

        RunAll = ReactiveCommand.CreateRunInBackground(async () => {
            var codeGeneration = codeGenerationProvider
                .GetGenerators()
                .Where(x => GenerationResults.All(y => y.Result?.Generator != x.Identifier))
                .Select(codeGenerator => {
                    var resultVM = taskEvaluatorVMFactory.CodeGenerationResultVM(taskSet, codeGenerator);
                    Dispatcher.UIThread.Post(() => GenerationResults.Add(resultVM));
                    return resultVM.GenerateAndEvaluate();
                })
                .ToList();

            await Task.WhenAll(codeGeneration);
        });
    }
}

public sealed class DesignTaskVM : ViewModel, ITaskVM {
    public TaskSet TaskSet => null!;
    public string Name { get; }
    public IObservable<TaskState> CurrentState { get; } = Observable.Return(TaskState.NotStarted);
    public ReactiveCommand<Unit, Task> RunCodeGeneration { get; }
    public ReactiveCommand<Unit, Task> RunAll { get; }
    public IObservableCollection<ICodeGenerationResultVM> GenerationResults { get; } = new ObservableCollectionExtended<ICodeGenerationResultVM>();

    public DesignTaskVM(int index) {
        Name = $"Task{index}";
        RunCodeGeneration = RunAll = ReactiveCommand.Create(() => {
            GenerationResults.AddRange(Enumerable.Range(0, 2)
                .Select(i => new DesignCodeGenerationResultVM($"Generator{i}")));

            return Task.CompletedTask;
        });
    }
}
