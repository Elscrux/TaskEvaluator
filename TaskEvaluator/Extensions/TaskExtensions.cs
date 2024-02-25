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

    public static async IAsyncEnumerable<T> AwaitAll<T>(this IEnumerable<T> f, Func<T, Task<T>> taskSelector, int parallelism, [EnumeratorCancellation] CancellationToken token = default) {
        var todo = new Queue<T>(f);

        var tasks = new List<Task<T>>();
        while (todo.Count > 0) {
            if (token.IsCancellationRequested) yield break;

            while (tasks.Count < parallelism && todo.Count > 0) {
                var item = todo.Dequeue();
                tasks.Add(taskSelector(item));
            }

            var completedTask = await Task.WhenAny(tasks);
            tasks.Remove(completedTask);

            yield return await completedTask;
        }
    }

    public static async Task AwaitAll<T>(this IEnumerable<T> f, Func<T, Task> taskSelector, int parallelism, CancellationToken token = default) {
        var todo = new Queue<T>(f);

        var tasks = new List<Task>();
        while (todo.Count > 0) {
            if (token.IsCancellationRequested) return;

            while (tasks.Count < parallelism && todo.Count > 0) {
                var item = todo.Dequeue();
                tasks.Add(taskSelector(item));
            }

            var completedTask = await Task.WhenAny(tasks);
            tasks.Remove(completedTask);
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
