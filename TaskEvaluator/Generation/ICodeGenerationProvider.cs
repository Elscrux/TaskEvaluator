namespace TaskEvaluator.Generation;

public interface ICodeGenerationProvider {
    IEnumerable<ICodeGenerator> GetGenerators();
}
