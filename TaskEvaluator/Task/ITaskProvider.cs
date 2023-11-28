namespace TaskEvaluator.Task;

public interface ITaskProvider {
    IEnumerable<TaskEvaluationModel> GetTasks();
}
