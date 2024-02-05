using TaskEvaluator.Language;
namespace TaskEvaluator.SonarQube.Scanner;

public interface ISonarScannerApiFactory {
    ISonarScannerApi Create(ProgrammingLanguage language);
}
