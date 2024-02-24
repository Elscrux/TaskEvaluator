// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using TaskEvaluator.HumanEvalConverter.DataModel;
using CSharpConverter = TaskEvaluator.HumanEvalConverter.Converter.Language.CSharpConverter;

var data = File.ReadAllText("human-eval-v2-20210705.json");
var tasks = JsonSerializer.Deserialize<HumanEvalTask[]>(data);
if (tasks == null) {
    throw new ArgumentException("Failed to deserialize tasks.");
}

List<CSharpConverter> converter = [new CSharpConverter()];

List<string> programs = [];
List<string> unitTests = [];
foreach (var humanEvalTask in tasks) {
    var task = humanEvalTask.ToConversionTask();

    foreach (var c in converter) {
        string program;
        try {
            program = c.GetProgram(task);
        } catch (Exception e) {
            Console.WriteLine($"Skipped task: {task.FunctionSignature.Name} in {c.Language} when parsing program because of error: {e.Message}");
            continue;
        }
        string unitTest;
        try {
            unitTest = c.GetUnitTest(task);
        } catch (Exception e) {
            Console.WriteLine($"Skipped task: {task.FunctionSignature.Name} in {c.Language} when parsing unit tests because of error: {e.Message}");
            continue;
        }

        var taskFolder = Path.Combine(c.Language.ToString(), c.CaseConverter.FromSnakeCase(task.FunctionSignature.Name));
        var programFile = Path.Combine(taskFolder, "Program" + c.Extension);
        var unitTestFile = Path.Combine(taskFolder, "UnitTests" + c.Extension);
        var metadataFile = Path.Combine(taskFolder, "metadata.json");

        Directory.CreateDirectory(taskFolder);
        File.WriteAllText(programFile, program);
        File.WriteAllText(unitTestFile, unitTest);
        File.WriteAllText(metadataFile, GetMetadata(humanEvalTask));
        programs.Add(program);
        unitTests.Add(unitTest);
    }
}

string GetMetadata(HumanEvalTask task) {
    return $$"""
             {
                 "id": "{{Guid.NewGuid()}}",
                 "isHumanEval": true
             }
             """;
}
