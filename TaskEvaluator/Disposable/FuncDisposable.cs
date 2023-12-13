namespace TaskEvaluator.Disposable;

public sealed class FuncDisposable(Action action) : IDisposable {
    public void Dispose() {
        action();
    }
}
