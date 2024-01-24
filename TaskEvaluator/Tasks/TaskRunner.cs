﻿using System.Runtime.CompilerServices;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Extensions;
using TaskEvaluator.Generation;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Tasks;

public sealed class TaskRunner(
    LanguageFactory languageFactory,
    IEvaluatorProvider evaluatorProvider,
    ICodeGenerationProvider codeGenerationProvider) {

    public async IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, [EnumeratorCancellation] CancellationToken token = default) {
        using var runtime = await languageFactory.CreateRuntime(code, token);

        foreach (var evaluator in evaluatorProvider.GetEvaluators(evaluationModel, runtime)) {
            await foreach (var evaluationResult in evaluator.Evaluate(code, evaluationModel, token)) {
                yield return evaluationResult;
            }
        }
    }

    public async IAsyncEnumerable<CodeGenerationResult> Generate(CodeGenerationTask task, [EnumeratorCancellation] CancellationToken token = default) {
        var models = codeGenerationProvider.GetGenerators().ToList();

        var tasks = models
            .Select(model => model.Send(task, token))
            .ToList();

        await foreach (var result in tasks.AwaitAll(token)) yield return result;
    }

    public async IAsyncEnumerable<IEvaluationResult> Process(TaskSet taskSet, [EnumeratorCancellation] CancellationToken token = default) {
        await foreach (var codeGenerationResult in Generate(taskSet.CodeGenerationTask, token)) {
            await foreach (var evaluationResult in Evaluate(codeGenerationResult.Code, taskSet.EvaluationModel, token)) {
                yield return evaluationResult;
            }
        }
    }
}
