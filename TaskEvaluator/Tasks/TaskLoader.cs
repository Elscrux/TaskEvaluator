using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public interface ITaskLoader {
    IEnumerable<TaskSet> Load();
}

public sealed class LocalTaskLoader(string input) : ITaskLoader {
    public IEnumerable<TaskSet> Load() {
        yield return new TaskSet(new CodeGenerationTask("", "", ProgrammingLanguage.CSharp), new EvaluationModel([]));
    }
}
