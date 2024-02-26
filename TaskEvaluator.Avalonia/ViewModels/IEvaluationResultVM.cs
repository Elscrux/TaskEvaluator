using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.SyntaxValidation;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public interface IEvaluationResultVM {
    IEvaluationResult? Result { get; set; }
    IObservable<TaskState> EvaluationCompleted { get; }
}

public sealed class FinishedEvaluationResultVM(IEvaluationResult result) : ViewModel, IEvaluationResultVM {
    [Reactive] public IEvaluationResult? Result { get; set; } = result;
    public IObservable<TaskState> EvaluationCompleted { get; } = Observable.Return(result.IsValid() ? TaskState.Success : TaskState.Fail);
}

public sealed class EvaluationResultVM : ViewModel, IEvaluationResultVM {
    private readonly IEvaluator _evaluator;
    private readonly Code _code;
    private readonly EvaluationModel _evaluationModel;

    [Reactive] public IEvaluationResult? Result { get; set; }
    public IObservable<TaskState> EvaluationCompleted { get; }

    public EvaluationResultVM(IEvaluator evaluator, Code code, EvaluationModel evaluationModel) {
        _evaluator = evaluator;
        _code = code;
        _evaluationModel = evaluationModel;
        EvaluationCompleted = this.WhenAnyValue(x => x.Result)
            .Select(x => {
                if (x is null) return TaskState.NotStarted;

                return x.IsValid() ? TaskState.Success : TaskState.Fail;
            });
    }

    public async Task<IEvaluationResult> Evaluate(CancellationToken token = default) {
        var result = await _evaluator.Evaluate(_code, _evaluationModel, token);
        Dispatcher.UIThread.Post(() => Result = result);
        return result;
    }
}

public sealed class DesignEvaluationResultVM(string evaluator) : ViewModel, IEvaluationResultVM {
    public IObservable<TaskState> EvaluationCompleted { get; } = Observable.Return(TaskState.Success);
    public DesignEvaluationResultVM() : this("Evaluator") {}
    public IEvaluationResult? Result { get; set; } = new SyntaxValidationResult(Guid.NewGuid(), Random.Shared.Next(0, 2) == 1, evaluator, null, true);
}
