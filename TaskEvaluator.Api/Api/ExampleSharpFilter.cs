using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Api.Api;

public sealed class ExampleSharpFilter : ISchemaFilter {
    public void Apply(OpenApiSchema schema, SchemaFilterContext context) {
        if (context.MemberInfo is null) return;

        schema.Example = context.MemberInfo.Name switch {
            nameof(Code.Body) => new OpenApiString("""
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
                                                   """),
            nameof(Code.Language) => new OpenApiInteger(1), //new OpenApiString(ProgrammingLanguage.CSharp.ToString()),
            nameof(EntryPoint.FullPath) => new OpenApiString("Task.TaskClass.GetWeekday"),
            nameof(EntryPoint.Parameters) => new OpenApiArray(),
            _ => schema.Example
        };
    }
}
