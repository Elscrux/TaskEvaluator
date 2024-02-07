using TaskEvaluator.Language;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime.Implementation.CSharp;

public sealed class CSharpService : ILanguageService {
    private const string RegularCsproj
        = """
          <Project Sdk="Microsoft.NET.Sdk">
          
              <PropertyGroup>
                  <TargetFramework>net8.0</TargetFramework>
                  <ImplicitUsings>enable</ImplicitUsings>
                  <Nullable>enable</Nullable>
              </PropertyGroup>

          </Project>
          """;
    private const string TestCsproj
        = """
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

    public ProgrammingLanguage Language => ProgrammingLanguage.CSharp;
    public string FileExtension => ".cs";
    public string LineCommentSymbol => "//";

    public void CreateWorkingDirectory(string workingDirectory, Code code) {
        Directory.CreateDirectory(workingDirectory);
        var project = Path.Combine(workingDirectory, "CSharpTemplateProject.csproj");
        File.WriteAllText(project, RegularCsproj);

        var taskClassPath = Path.Combine(workingDirectory, "Program.cs");
        File.WriteAllText(taskClassPath, code.Body);
    }
    public void CreateTestDirectory(string testDirectory, Code code) {
        Directory.CreateDirectory(testDirectory);
        var project = Path.Combine(testDirectory, "CSharpTemplateProject.csproj");
        File.WriteAllText(project, TestCsproj);

        var taskClassPath = Path.Combine(testDirectory, "Program.cs");
        File.WriteAllText(taskClassPath, code.Body);
    }
}
