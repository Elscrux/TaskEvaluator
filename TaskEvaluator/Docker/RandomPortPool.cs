namespace TaskEvaluator.Docker;

public sealed class RandomPortPool : IPortPool {
    private readonly List<int> _ports = [];

    public int AllocatePort() {
        var port = GetRandomPort();
        _ports.Add(port);
        return port;
    }

    private int GetRandomPort() {
        var random = new Random();
        var port = random.Next(10000, 20000);
        while (_ports.Contains(port)) {
            port = random.Next(10000, 20000);
        }
        return port;
    }

    public void ReleasePort(int port) {
        _ports.Remove(port);
    }
}
