using System.Runtime.CompilerServices;
namespace TaskEvaluator;

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

    public static async IAsyncEnumerable<T> Merge<T>(this IEnumerable<IAsyncEnumerable<T>> enumerables, [EnumeratorCancellation] CancellationToken token = default) {
        var tasks = enumerables
            .Select(async source => await source.ToListAsync(token))
            .ToList();

        while (tasks.Count > 0) {
            if (token.IsCancellationRequested) yield break;

            var completedTask = await Task.WhenAny(tasks);
            tasks.Remove(completedTask);

            foreach (var result in completedTask.Result) {
                yield return result;
            }
        }
    }
}
