using System.Linq;
using DynamicData.Binding;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public sealed class RunnerVM : ViewModel, IRunnerVM {
    public IObservableCollection<ITaskVM> Tasks { get; }

    public RunnerVM(
        LanguageFactory languageFactory,
        IEvaluatorProvider evaluatorProvider,
        ICodeGenerationProvider codeGenerationProvider,
        ITaskLoader taskLoader) {
        Tasks = new ObservableCollectionExtended<ITaskVM>(taskLoader.Load().Select(task => new TaskVM(languageFactory, evaluatorProvider, codeGenerationProvider, task)));
    }
}

public sealed class DesignRunnerVM : ViewModel, IRunnerVM {
    public IObservableCollection<ITaskVM> Tasks { get; }

    public DesignRunnerVM() {
        Tasks = new ObservableCollectionExtended<ITaskVM>(
            Enumerable
                .Range(0, 10)
                .Select(i => new DesignTaskVM(i)));
    }
}

public interface IRunnerVM {
    IObservableCollection<ITaskVM> Tasks { get; }
}
