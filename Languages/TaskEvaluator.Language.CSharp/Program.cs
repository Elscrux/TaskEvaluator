using System.Diagnostics;
using TaskEvaluator.Runtime;
using TaskEvaluator.Runtime.Implementation.CSharp;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Language.CSharp;

public static class Program {
    public static void Main(string[] args) {
        var code = Environment.GetEnvironmentVariable("CODE") ?? """
                                                                 public static int GetWeekday(int year, int month, int day) {
                                                                     var date = new DateTime(year, month, day);
                                                                     return date.DayOfWeek switch {
                                                                         DayOfWeek.Monday => 0,
                                                                         DayOfWeek.Tuesday => 1,
                                                                         DayOfWeek.Wednesday => 2,
                                                                         DayOfWeek.Thursday => 3,
                                                                         DayOfWeek.Friday => 4,
                                                                         DayOfWeek.Saturday => 5,
                                                                         DayOfWeek.Sunday => 6,
                                                                         _ => throw new ArgumentOutOfRangeException(nameof(date.DayOfWeek))
                                                                     };
                                                                 }
                                                                 """;

        var file = Path.Combine(TemplateProjectDirectory, "TaskClass.cs");
        var program = /*File
            .ReadAllText(Path.Combine(file))*/
            """
                namespace Task;

                public static class TaskClass {
                    // Replace this with generated code
                }
                """
                .Replace("// Replace this with generated code", code);

        var directoryName = Path.GetDirectoryName(file);
        Directory.CreateDirectory(directoryName);

        var directoryAttributes = File.GetAttributes(directoryName);
        Console.WriteLine(directoryAttributes);
        Console.WriteLine();
        foreach (var fileAttributes in Enum.GetValues<FileAttributes>()) {
            Console.WriteLine(fileAttributes + (directoryAttributes.HasFlag(fileAttributes)).ToString());
        }

        var attributes = File.GetAttributes(file);
        Console.WriteLine(attributes);
        Console.WriteLine();
        foreach (var fileAttributes in Enum.GetValues<FileAttributes>()) {
            Console.WriteLine(fileAttributes + (attributes.HasFlag(fileAttributes)).ToString());
        }


        File.WriteAllText(file, program);

        StartUnitTest(new Code(Guid.NewGuid(), """
                                               using Xunit;
                                               namespace Task;

                                               public static class TestClass {
                                                   [Fact]
                                                   public static void Test_2023_2_3() {
                                                       var weekday = TaskClass.GetWeekday(2023, 2, 3);
                                                       Assert.Equal(4, weekday);
                                                   }
                                               }
                                               """, new EntryPoint("TestClass.Test_2023_2_3", []), ProgrammingLanguage.CSharp));

        var builder = WebApplication.CreateBuilder(args);

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

    private const string TemplateProjectDirectory = "./csharp-template-project";
    private const string TestDirectory = "./tests";
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
            Arguments = "test",
            WorkingDirectory = jobFolder,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        });

        if (process == null) {
            return new CSharpRuntimeResult(false, "Failed to start dotnet test");
        }

        // read the output
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();

        // wait for the process to exit
        process.WaitForExit();

        Console.WriteLine(output);

        // return the result
        return new CSharpRuntimeResult(process.ExitCode == 0, output + error);
    }
}
