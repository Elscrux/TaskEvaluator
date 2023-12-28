using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Language.CSharp;

public static class Program {
    private const string Csproj = """
                                  <Project Sdk="Microsoft.NET.Sdk">
                                  
                                      <PropertyGroup>
                                          <TargetFramework>net8.0</TargetFramework>
                                          <ImplicitUsings>enable</ImplicitUsings>
                                          <Nullable>enable</Nullable>
                                  
                                          <IsPackable>false</IsPackable>
                                          <IsTestProject>true</IsTestProject>
                                      </PropertyGroup>
                                  
                                      <ItemGroup>
                                          <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.6.0"/>
                                          <PackageReference Include="xunit" Version="2.4.2"/>
                                          <PackageReference Include="xunit.runner.visualstudio" Version="2.4.5">
                                              <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
                                              <PrivateAssets>all</PrivateAssets>
                                          </PackageReference>
                                          <PackageReference Include="coverlet.collector" Version="6.0.0">
                                              <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
                                              <PrivateAssets>all</PrivateAssets>
                                          </PackageReference>
                                      </ItemGroup>

                                  </Project>
                                  """;

    private const string ReplaceComment = "// Replace this with generated code";
    private const string Task = $$"""
                                  namespace Task;

                                  public static class TaskClass {
                                      {{ReplaceComment}}
                                  }
                                  """;

    private const string WorkingDirectory = "/home/app";
    private static readonly string TemplateProjectDirectory = Path.Combine(WorkingDirectory, "./csharp-template-project");
    private static readonly string TestDirectory = Path.Combine(WorkingDirectory, "./tests");

    private static void InitTemplateDirectory() {
        // input code ist stored in env variable CODE
        var code = Environment.GetEnvironmentVariable("CODE")
         ?? throw new InvalidOperationException("CODE environment variable not set");

        Directory.CreateDirectory(TemplateProjectDirectory);

        // Create project file
        var project = Path.Combine(TemplateProjectDirectory, "CSharpTemplateProject.csproj");
        File.WriteAllText(project, Csproj);

        // Create task class where the code lies, insert the code into it
        var taskClassPath = Path.Combine(TemplateProjectDirectory, "TaskClass.cs");
        var taskClassContent = Task.Replace(ReplaceComment, code);
        File.WriteAllText(taskClassPath, taskClassContent);
    }

    public static void Main(string[] args) {
        InitTemplateDirectory();

        var builder = WebApplication.CreateSlimBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

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

        app.Run();
    }

    private static IRuntimeResult StartUnitTest(Code unitTest) {
        // copy content of TemplateProjectDirectory to a jobs directory and create a new job folder with a unique name (guid)

        // create a new job folder with a unique name (guid)
        var jobFolder = Path.Combine(TestDirectory, Guid.NewGuid().ToString());
        Directory.CreateDirectory(jobFolder);

        // copy content of TemplateProjectDirectory to a jobs directory
        foreach (var file in Directory.EnumerateFiles(TemplateProjectDirectory)) {
            File.Copy(file, Path.Combine(jobFolder, Path.GetFileName(file)));
        }

        // copy unit test code to the job folder
        File.WriteAllText(Path.Combine(jobFolder, "UnitTest.cs"), unitTest.Body);

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
