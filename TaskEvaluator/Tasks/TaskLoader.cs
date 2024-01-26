namespace TaskEvaluator.Tasks;

public interface ITaskLoader {
    IEnumerable<TaskSet> Load();
}
