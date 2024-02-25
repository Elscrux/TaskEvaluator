using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TaskEvaluator.Sink.PostgreSQL;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public sealed class RunnerVM : ViewModel, IRunnerVM {
    public IObservableCollection<ITaskVM> Tasks { get; }
    public ReactiveCommand<Unit, Task> RunAll { get; }
    public ReactiveCommand<Unit, Unit> LoadFromDatabase { get; }

    [Reactive] public int Parallelism { get; set; } = 5;

    public RunnerVM(
        PostgresFinalResultSink postgresFinalResultSink,
        TaskEvaluatorVMFactory taskEvaluatorVMFactory,
        ITaskLoader taskLoader) {
        Tasks = new ObservableCollectionExtended<ITaskVM>(taskLoader.Load().Select(taskEvaluatorVMFactory.TaskVM));

        RunAll = ReactiveCommand.CreateRunInBackground(async () => {
            await Tasks.AwaitAll(async vm => {
                await await vm.RunAll.Execute();
            }, Parallelism);
        });

        LoadFromDatabase = ReactiveCommand.CreateRunInBackground(() => {
            var taskResults = postgresFinalResultSink.Retrieve()
                .GroupBy(x => x.CodeGenerationResult.TaskId)
                .ToDictionary(x => x.Key, x => x);

            foreach (var task in Tasks) {
                if (task.GenerationResults.Count > 0) continue;
                if (!taskResults.TryGetValue(task.TaskSet.TaskMetadata.TaskId, out var results)) continue;

                foreach (var finalResult in results) {
                    var codeGenerationResultVM = new FinishedCodeGenerationResultVM(finalResult);
                    Dispatcher.UIThread.Post(() => task.GenerationResults.Add(codeGenerationResultVM));
                }
            }
        });
    }
}

public sealed class DesignRunnerVM : ViewModel, IRunnerVM {
    public IObservableCollection<ITaskVM> Tasks { get; }
    public int Parallelism { get; set; }

    public ReactiveCommand<Unit, Task> RunAll { get; } = ReactiveCommand.Create(() => Task.CompletedTask);
    public ReactiveCommand<Unit, Unit> LoadFromDatabase { get; } = ReactiveCommand.Create(() => {});

    public DesignRunnerVM() {
        Tasks = new ObservableCollectionExtended<ITaskVM>(
            Enumerable
                .Range(0, 10)
                .Select(i => new DesignTaskVM(i)));
    }
}

public interface IRunnerVM {
    IObservableCollection<ITaskVM> Tasks { get; }
    int Parallelism { get; set; }

    public ReactiveCommand<Unit, Task> RunAll { get; }
    ReactiveCommand<Unit, Unit> LoadFromDatabase { get; }
}
