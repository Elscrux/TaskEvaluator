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
    EvaluationModel EvaluationModel { get; }
}

public sealed class EvaluationResultVM(IEvaluator evaluator, Code code, EvaluationModel evaluationModel) : ViewModel, IEvaluationResultVM {
    [Reactive] public IEvaluationResult? Result { get; set; }
    public EvaluationModel EvaluationModel { get; } = evaluationModel;

    public async Task Evaluate(CancellationToken token = default) {
        var result = await evaluator.Evaluate(code, EvaluationModel, token);
        Dispatcher.UIThread.Post(() => Result = result);
    }
}

public sealed class DesignEvaluationResultVM(string evaluator) : ViewModel, IEvaluationResultVM {
    public IEvaluationResult? Result { get; set; } = new SyntaxValidationResult(Guid.NewGuid(), Random.Shared.Next(0, 2) == 1, evaluator, null, true);
    public EvaluationModel EvaluationModel { get; } = new(new Code(string.Empty, ProgrammingLanguage.CSharp));
}
