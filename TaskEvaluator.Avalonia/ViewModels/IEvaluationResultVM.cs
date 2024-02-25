using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI.Fody.Helpers;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.SyntaxValidation;
using TaskEvaluator.Language;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public interface IEvaluationResultVM {
    IEvaluationResult? Result { get; set; }
}

public sealed class FinishedEvaluationResultVM(IEvaluationResult result) : ViewModel, IEvaluationResultVM {
    [Reactive] public IEvaluationResult? Result { get; set; } = result;
}

public sealed class EvaluationResultVM(IEvaluator evaluator, Code code, EvaluationModel evaluationModel) : ViewModel, IEvaluationResultVM {
    [Reactive] public IEvaluationResult? Result { get; set; }

    public async Task<IEvaluationResult> Evaluate(CancellationToken token = default) {
        var result = await evaluator.Evaluate(code, evaluationModel, token);
        Dispatcher.UIThread.Post(() => Result = result);
        return result;
    }
}

public sealed class DesignEvaluationResultVM(string evaluator) : ViewModel, IEvaluationResultVM {
    public DesignEvaluationResultVM() : this("Evaluator") {}
    public IEvaluationResult? Result { get; set; } = new SyntaxValidationResult(Guid.NewGuid(), Random.Shared.Next(0, 2) == 1, evaluator, null, true);
}
