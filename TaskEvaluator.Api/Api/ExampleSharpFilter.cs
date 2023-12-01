using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Api.Api;

public sealed class ExampleSharpFilter : ISchemaFilter {
    public void Apply(OpenApiSchema schema, SchemaFilterContext context) {
        if (context.MemberInfo is null) return;

        schema.Example = context.MemberInfo.Name switch {
            nameof(Code.Body) => new OpenApiString("using System;\nnamespace Test;\n\npublic class X {\n    public string Run() {\n        return \"Hello World!\";\n    }\n}\n"),
            nameof(Code.Language) => new OpenApiInteger(1), //new OpenApiString(ProgrammingLanguage.CSharp.ToString()),
            nameof(EntryPoint.FullPath) => new OpenApiString("Test.X.Run"),
            nameof(EntryPoint.Parameters) => new OpenApiArray(),
            _ => schema.Example
        };
    }
}
