namespace TaskEvaluator.Docker;

public sealed class DockerHostFactory {
    private const string Hostname = "localhost";

    public DockerHost Create(int port) => new(Hostname, port);
}
