using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskEvaluator.Generation;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Api.Api;

public sealed class ExampleSharpFilter : ISchemaFilter {
    public void Apply(OpenApiSchema schema, SchemaFilterContext context) {
        if (context.MemberInfo is null) return;

        schema.Example = context.MemberInfo.Name switch {
            nameof(Code.Body) => new OpenApiString("""
                                                   using Xunit;
                                                   namespace Task;

                                                   public class Test {
                                                       [Fact]
                                                       public void Test_2023_12_31() {
                                                           var weekday = TaskClass.GetWeekday(2023, 12, 31);
                                                           Assert.Equal(0, weekday);
                                                       }
                                                   }
                                                   """),
            nameof(Code.Language) => new OpenApiInteger(1), //new OpenApiString(ProgrammingLanguage.CSharp.ToString()),
            nameof(CodeGenerationTask.Prefix) => new OpenApiString("""
                                                                   namespace Task;

                                                                   public static class TaskClass {
                                                                       /// method that calculates the weekday of a given date
                                                                       public static int GetWeekday(int year, int month, int day) {
                                                                   """),
            nameof(CodeGenerationTask.Suffix) => new OpenApiString("""
                                                                   }
                                                                   """),
            _ => schema.Example
        };
    }
}
