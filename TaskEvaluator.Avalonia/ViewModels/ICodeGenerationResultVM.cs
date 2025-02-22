using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData.Binding;
using Noggog;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Language;
using TaskEvaluator.Runtime;
using TaskEvaluator.Sinks;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public interface ICodeGenerationResultVM {
    IObservable<TaskState> EvaluationCompleted { get; }
    CodeGenerationResult? Result { get; set; }
    IObservableCollection<IEvaluationResultVM> EvaluationResults { get; }
    ReactiveCommand<Unit, Task> RunEvaluation { get; }
}

public sealed class CodeGenerationResultVM : ViewModel, ICodeGenerationResultVM {
    private readonly TaskRetry _taskRetry;
    private readonly TaskSet _taskSet;
    private readonly ICodeGenerator _codeGenerator;
    private readonly LanguageFactory _languageFactory;
    private readonly IEvaluatorProvider _evaluatorProvider;
    private readonly List<IFinalResultSink> _resultSinks;

    public IObservable<TaskState> EvaluationCompleted { get; }
    [Reactive] public CodeGenerationResult? Result { get; set; }
    public IObservableCollection<IEvaluationResultVM> EvaluationResults { get; } = new ObservableCollectionExtended<IEvaluationResultVM>();
    private readonly IObservableCollection<IEvaluationResultVM> _latestResults = new ObservableCollectionExtended<IEvaluationResultVM>();
    public ReactiveCommand<Unit, Task> RunEvaluation { get; }

    public CodeGenerationResultVM(
        IEnumerable<IFinalResultSink> finalResultSinks,
        TaskSet taskSet,
        TaskRetry taskRetry,
        ICodeGenerator codeGenerator,
        LanguageFactory languageFactory,
        IEvaluatorProvider evaluatorProvider) {
        _taskSet = taskSet;
        _taskRetry = taskRetry;
        _codeGenerator = codeGenerator;
        _languageFactory = languageFactory;
        _evaluatorProvider = evaluatorProvider;
        _resultSinks = finalResultSinks.ToList();
        EvaluationCompleted = _latestResults
            .ObserveCollectionChanges()
            .Select(_ => {
                if (EvaluationResults.Count == 0) return Observable.Return(TaskState.NotStarted);
                if (_latestResults.Count == 0) return Observable.Return(TaskState.Running);

                return _latestResults
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

        RunEvaluation = ReactiveCommand.CreateRunInBackground(async () => {
            if (Result is null) return;

            await Evaluate(Result);
        });
    }

    public async Task<CodeGenerationResult> Generate(CodeGenerationTask codeGenerationTask, CancellationToken token = default) {
        var result = await _codeGenerator.Send(codeGenerationTask, token);
        Dispatcher.UIThread.Post(() => Result = result);

        return result;
    }

    public async Task<FinalResult> GenerateAndEvaluate(CancellationToken token = default) {
        var result = await Generate(_taskSet.CodeGenerationTask, token);

        return await Evaluate(result, token);
    }

    public async Task<FinalResult> Evaluate(CodeGenerationResult result, CancellationToken token = default) {
        var usedInitialResult = false;
        var finalResults = await _taskRetry.Try(_taskSet, async x => {
            if (usedInitialResult) {
                result = await Generate(x.Task, token) with { RetryCount = x.CurrentTry };
            } else {
                usedInitialResult = true;
            }

            using var runtime = await _languageFactory.CreateRuntime(result.Code, token);

            var evaluators = await _evaluatorProvider
                .GetEvaluators(_taskSet.EvaluationModel, runtime)
                .ToListAsync(token);

            Dispatcher.UIThread.Post(() => _latestResults.Clear());

            var enumerable = evaluators
                .Select(async evaluator => {
                    var evaluationResultVM = new EvaluationResultVM(evaluator, result.Code, _taskSet.EvaluationModel);
                    Dispatcher.UIThread.Post(() => {
                        EvaluationResults.Add(evaluationResultVM);
                        _latestResults.Add(evaluationResultVM);
                    });
                    return await evaluationResultVM.Evaluate(token);
                });

            var evaluationResults = await Task.WhenAll(enumerable);
            var finalResult = new FinalResult(result, evaluationResults.NotNull().ToList());

            foreach (var sink in _resultSinks) {
                sink.Send(finalResult);
            }

            return [finalResult];
        });

        return finalResults[0];
    }
}

public sealed class FinishedCodeGenerationResultVM(FinalResult finalResult) : ViewModel, ICodeGenerationResultVM {
    public IObservable<TaskState> EvaluationCompleted { get; } = Observable.Return(TaskState.Success);
    [Reactive] public CodeGenerationResult? Result { get; set; } = finalResult.CodeGenerationResult;
    public IObservableCollection<IEvaluationResultVM> EvaluationResults { get; } = new ObservableCollectionExtended<IEvaluationResultVM>(finalResult.EvaluationResults.Select(result => new FinishedEvaluationResultVM(result)));
    public ReactiveCommand<Unit, Task> RunEvaluation { get; } = ReactiveCommand.Create(() => Task.CompletedTask);
}

public sealed class DesignCodeGenerationResultVM(string generator) : ICodeGenerationResultVM {
    public IObservable<TaskState> EvaluationCompleted { get; } = Observable.Return(TaskState.Success);
    public CodeGenerationResult? Result { get; set; } = new(
        Guid.NewGuid(),
        Random.Shared.Next(0, 2) == 1,
        new Code(
            new CodeGenerationTask(Guid.NewGuid(), string.Empty, string.Empty, ProgrammingLanguage.CSharp),
            string.Empty),
        string.Empty,
        generator,
        TimeSpan.FromSeconds(1));
    public IObservableCollection<IEvaluationResultVM> EvaluationResults { get; }
        = new ObservableCollectionExtended<IEvaluationResultVM>(
            Enumerable.Range(0, 2)
                .Select(i => new DesignEvaluationResultVM($"Evaluator {i}"))
        );
    public ReactiveCommand<Unit, Task> RunEvaluation { get; } = ReactiveCommand.Create(() => Task.CompletedTask);
}
