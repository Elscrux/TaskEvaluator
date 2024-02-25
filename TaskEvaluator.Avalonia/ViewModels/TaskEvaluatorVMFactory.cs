using System.Collections.Generic;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Runtime;
using TaskEvaluator.Sinks;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Avalonia.ViewModels;

public sealed class TaskEvaluatorVMFactory(
    TaskRetry taskRetry,
    LanguageFactory languageFactory,
    IEnumerable<IFinalResultSink> finalResultSinks,
    ICodeGenerationProvider codeGenerationProvider,
    IEvaluatorProvider evaluatorProvider) {

    public CodeGenerationResultVM CodeGenerationResultVM(TaskSet taskSet, ICodeGenerator codeGenerator) => new(finalResultSinks, taskSet, taskRetry, codeGenerator, languageFactory, evaluatorProvider);
    public TaskVM TaskVM(TaskSet taskSet) => new(this, codeGenerationProvider, taskSet);
}
