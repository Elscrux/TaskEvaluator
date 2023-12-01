using TaskEvaluator.Runtime.Exceptions;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public sealed class FuncRuntime(Code code, Func<CancellationToken, Task<IRuntimeResult>> run) : IRuntime {
    public Code Context { get; } = code;

    public Task<IRuntimeResult> Run(CancellationToken token = default) {
        try {
            return run(token);
        } catch (Exception e) {
            throw new RuntimeFailedException(e.Message);
        }
    }
}
