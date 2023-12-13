using TaskEvaluator.Tasks;
namespace TaskEvaluator.Language.CSharp;

public static class Program {
    public static void Main(string[] args) {
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

        void StartUnitTest(Code unitTest) {
            Console.WriteLine($"Unit test: {unitTest.Body} ({unitTest.Guid}) {unitTest.EntryPoint} {unitTest.Language}");
        }
    }
}
