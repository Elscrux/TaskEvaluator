using System.Linq;
using System.Reactive;
using Avalonia.Threading;
using DynamicData.Binding;
using ReactiveUI;
using TaskEvaluator.Sink.PostgreSQL;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public sealed class RunnerVM : ViewModel, IRunnerVM {
    public IObservableCollection<ITaskVM> Tasks { get; }
    public ReactiveCommand<Unit, Unit> LoadFromDatabase { get; }

    public RunnerVM(
        PostgresFinalResultSink postgresFinalResultSink,
        TaskEvaluatorVMFactory taskEvaluatorVMFactory,
        ITaskLoader taskLoader) {
        Tasks = new ObservableCollectionExtended<ITaskVM>(taskLoader.Load().Select(taskEvaluatorVMFactory.TaskVM));
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
    ReactiveCommand<Unit, Unit> LoadFromDatabase { get; }
}
