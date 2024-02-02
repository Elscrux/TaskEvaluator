using TaskEvaluator.HumanEvalConverter.DataModel;
using TaskEvaluator.Language;
namespace TaskEvaluator.HumanEvalConverter.Converter.Language;

public interface IHumanEvalConverter {
    ProgrammingLanguage Language { get; }
    string Extension { get; }
    string GetProgram(DataSetTask dataSetTask);
    string GetUnitTest(DataSetTask dataSetTask);
}
