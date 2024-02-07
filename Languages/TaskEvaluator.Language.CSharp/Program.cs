using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Modules;
using TaskEvaluator.Runtime;
using TaskEvaluator.Runtime.Implementation.CSharp;
using TaskEvaluator.SonarQube;
using TaskEvaluator.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
namespace TaskEvaluator.Language.CSharp;

public static class Program {
    private const string WorkingDirectory = "/home/app";
    private static readonly string TestDirectory = Path.Combine(WorkingDirectory, "./tests");

    public static Code Code = null!;

    private static void InitTemplateDirectory() {
        // input code ist stored in env variable CODE
        var code = Environment.GetEnvironmentVariable("CODE")
         ?? throw new InvalidOperationException("CODE environment variable not set");
        Code = JsonSerializer.Deserialize<Code>(code) ?? throw new InvalidOperationException($"Failed to deserialize CODE environment variable {code}");
    }

    public static void Main(string[] args) {
        InitTemplateDirectory();

        var builder = WebApplication.CreateSlimBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient();
        builder.Services.AddHealthChecks();

        builder.Services.AddTaskEvaluator(builder.Configuration);
        builder.Services.AddCSharp();
        builder.Services.AddSonarQube(builder.Configuration);

        builder.Configuration.AddUserSecrets<TaskRunner>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapHealthChecks("/health");

        app.MapPost("/unit-test", StartUnitTest)
            .WithName("Unit Test")
            .WithOpenApi();

        app.MapPost("/sonar-qube", RunSonarQube)
            .WithName("Sonar Qube")
            .WithOpenApi();

        app.Run();

        async IAsyncEnumerable<IEvaluationResult> RunSonarQube(HttpContext c, [EnumeratorCancellation] CancellationToken token = default) {
            Console.WriteLine("Got SonarQube");
            var evaluatorFactory = c.RequestServices.GetRequiredService<Task<SonarQubeEvaluator?>>();
            var evaluator = await evaluatorFactory;
            if (evaluator == null) {
                throw new InvalidOperationException("Failed to get SonarQubeEvaluator");
            }

            await foreach (var evaluationResult in evaluator.Evaluate(Code, new EvaluationModel(), token)) {
                yield return evaluationResult;
            }
        }

        IRuntimeResult StartUnitTest(HttpContext c, Code unitTest) {
            // create a new job folder with a unique name (guid)
            var languageFactory = c.RequestServices.GetRequiredService<LanguageFactory>();
            var languageService = languageFactory.GetLanguageService(ProgrammingLanguage.CSharp);
            var jobFolder = Path.Combine(TestDirectory, Guid.NewGuid().ToString());
            languageService.CreateTestDirectory(jobFolder, Code, unitTest);

            // run the job via dotnet test
            var process = Process.Start(new ProcessStartInfo {
                FileName = "dotnet",
                Arguments = "test --logger \"trx;LogFileName=results.trx\"",
                WorkingDirectory = jobFolder,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            });

            if (process == null) {
                return new UnitTestRuntimeResult(false, "Failed to start dotnet test");
            }

            // read the output
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            // wait for the process to exit
            process.WaitForExit();

            // return results
            var trxPath = Path.Combine(jobFolder, "TestResults", "results.trx");

            // return plain output if trx file is not found
            if (!File.Exists(trxPath)) return new UnitTestRuntimeResult(process.ExitCode == 0, output + error);

            // parse the test results
            var testResults = ParseTestResults(trxPath).ToList();
            foreach (var r in testResults) {
                Console.WriteLine($"Test: {r.TestName}, Outcome: {r.Outcome}, Duration: {r.Duration}");
            }

            return new UnitTestRuntimeResult(process.ExitCode == 0, output + error, testResults);
        }
    }

    private static IEnumerable<UnitTestResult> ParseTestResults(string trxPath) {
        var trxContent = File.ReadAllText(trxPath);

        // Parse the XML
        var doc = XDocument.Parse(trxContent);
        var ns = XNamespace.Get("http://microsoft.com/schemas/VisualStudio/TeamTest/2010");
        var testResults = doc.Descendants(ns + "UnitTestResult");

        // Get test results
        foreach (var testResult in testResults) {
            Console.WriteLine(testResult);
            var testName = testResult.Attribute("testName")?.Value ?? "No-Name";
            var outcome = testResult.Attribute("outcome") is {} attr
                ? Enum.Parse<UnitTestOutcome>(attr.Value)
                : UnitTestOutcome.Failed;
            var duration = testResult.Attribute("duration")?.Value is {} x ? TimeSpan.Parse(x, new DateTimeFormatInfo()) : TimeSpan.Zero;

            yield return new UnitTestResult(testName, outcome, duration);
        }
    }
}
