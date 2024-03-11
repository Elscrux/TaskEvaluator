using System.Collections.Generic;
using System.Linq;
namespace TaskEvaluator.Avalonia.ViewModels;

public enum TaskState {
    NotStarted,
    Running,
    Success,
    Fail
}

public static class TaskStateExtensions {
    public static TaskState Merge(this IList<TaskState> states) {
        if (states.Any(s => s == TaskState.Fail)) return TaskState.Fail;
        if (states.Any(s => s == TaskState.Running)) return TaskState.Running;
        if (states.All(s => s == TaskState.Success)) return TaskState.Success;
        if (states.Any(s => s == TaskState.Success)) return TaskState.Running;

        return TaskState.NotStarted;
    }
}
