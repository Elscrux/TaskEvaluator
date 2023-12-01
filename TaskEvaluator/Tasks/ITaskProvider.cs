namespace TaskEvaluator.Tasks;

public interface ITaskProvider {
    IEnumerable<TaskEvaluationModel> GetTasks();
}
