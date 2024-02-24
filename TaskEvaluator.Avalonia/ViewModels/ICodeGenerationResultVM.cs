using System;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Language;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public interface ICodeGenerationResultVM {
    bool IsBusy { get; set; }
    CodeGenerationResult? Result { get; set; }
    IObservableCollection<IEvaluationResultVM> EvaluationResults { get; }
    public ReactiveCommand<Unit, Task> RunEvaluation { get; }
}

public sealed class CodeGenerationResultVM : ViewModel, ICodeGenerationResultVM {
    private readonly ICodeGenerator _codeGenerator;
    private readonly TaskSet _taskSet;
    private readonly LanguageFactory _languageFactory;
    private readonly IEvaluatorProvider _evaluatorProvider;

    [Reactive] public bool IsBusy { get; set; }
    [Reactive] public CodeGenerationResult? Result { get; set; }
    public IObservableCollection<IEvaluationResultVM> EvaluationResults { get; } = new ObservableCollectionExtended<IEvaluationResultVM>();
    public ReactiveCommand<Unit, Task> RunEvaluation { get; }

    public CodeGenerationResultVM(ICodeGenerator codeGenerator,
        TaskSet taskSet,
        LanguageFactory languageFactory,
        IEvaluatorProvider evaluatorProvider) {
        _codeGenerator = codeGenerator;
        _taskSet = taskSet;
        _languageFactory = languageFactory;
        _evaluatorProvider = evaluatorProvider;
        RunEvaluation = ReactiveCommand.CreateRunInBackground(async () => {
            if (Result is null) return;

            await Evaluate(Result);
        });
    }

    public async Task<CodeGenerationResult> Generate(CancellationToken token = default) {
        var result = await _codeGenerator.Send(_taskSet.CodeGenerationTask, token);
        Dispatcher.UIThread.Post(() => Result = result);

        return result;
    }

    public async Task GenerateAndEvaluate(CancellationToken token = default) {
        var result = await Generate(token);

        await Evaluate(result, token);
    }

    public async Task Evaluate(CodeGenerationResult result, CancellationToken token = default) {
        Dispatcher.UIThread.Post(() => IsBusy = true);

        using var runtime = await _languageFactory.CreateRuntime(result.Code, token);

        var evaluators = await _evaluatorProvider
            .GetEvaluators(_taskSet.EvaluationModel, runtime)
            .ToListAsync(token);

        var enumerable = evaluators
            .Select(evaluator => {
                var evaluationResultVM = new EvaluationResultVM(evaluator, result.Code, _taskSet.EvaluationModel);
                Dispatcher.UIThread.Post(() => EvaluationResults.Add(evaluationResultVM));
                return evaluationResultVM.Evaluate(token);
            });

        await Task.WhenAll(enumerable);

        Dispatcher.UIThread.Post(() => IsBusy = false);
    }
}

public sealed class DesignCodeGenerationResultVM(string generator) : ICodeGenerationResultVM {
    public bool IsBusy { get; set; }
    public CodeGenerationResult? Result { get; set; } = new(
        Guid.NewGuid(),
        Random.Shared.Next(0, 2) == 1,
        new Code(
            new CodeGenerationTask(Guid.NewGuid(), string.Empty, string.Empty, ProgrammingLanguage.CSharp),
            string.Empty),
        string.Empty,
        generator);
    public IObservableCollection<IEvaluationResultVM> EvaluationResults { get; }
        = new ObservableCollectionExtended<IEvaluationResultVM>(
            Enumerable.Range(0, 2)
                .Select(i => new DesignEvaluationResultVM($"Evaluator {i}"))
        );
    public ReactiveCommand<Unit, Task> RunEvaluation { get; } = ReactiveCommand.Create(() => Task.CompletedTask);
}
