using System.Runtime.CompilerServices;
namespace TaskEvaluator.Extensions;

public static class TaskExtensions {
    public static async IAsyncEnumerable<T> AwaitAll<T>(this IEnumerable<Task<T>> tasks, [EnumeratorCancellation] CancellationToken token = default) {
        var list = tasks.ToList();
        while (list.Count > 0) {
            if (token.IsCancellationRequested) yield break;

            var completedTask = await Task.WhenAny(list);
            list.Remove(completedTask);

            yield return await completedTask;
        }
    }
}
