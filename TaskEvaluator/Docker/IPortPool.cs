namespace TaskEvaluator.Docker;

public interface IPortPool {
    int AllocatePort();
    void ReleasePort(int port);
}
