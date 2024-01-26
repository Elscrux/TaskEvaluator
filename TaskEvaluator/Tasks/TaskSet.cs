using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
namespace TaskEvaluator.Tasks;

public sealed record TaskSet(
    Guid Guid,
    CodeGenerationTask CodeGenerationTask,
    EvaluationModel EvaluationModel) {
    public TaskSet(CodeGenerationTask codeGenerationTask, EvaluationModel evaluationModel)
        : this(Guid.NewGuid(), codeGenerationTask, evaluationModel) {}
}
