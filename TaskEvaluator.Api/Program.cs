using System.Text.Json.Serialization;
using TaskEvaluator.Api.Api;
using TaskEvaluator.Api.Requests;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
using TaskEvaluator.Modules;
using TaskEvaluator.Runtime.Implementation.CSharp;
using TaskEvaluator.SonarQube;
using TaskEvaluator.Tasks;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SchemaFilter<ExampleSharpFilter>());
builder.Services.AddLogging();
builder.Services.AddHttpClient();
builder.Services.AddHealthChecks();

builder.Configuration.AddUserSecrets<TaskRunner>();

builder.Services.AddTaskEvaluator();
builder.Services.AddCSharp();
builder.Services.AddSonarQube();

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapPost("/evaluate", EvaluateCode)
    .WithName("Evaluate")
    .WithOpenApi();

app.MapPost("/generate", GenerateCode)
    .WithName("Generate")
    .WithOpenApi();

app.MapPost("/full-pass", FullPass)
    .WithName("Full Pass")
    .WithOpenApi();

app.Run();

IAsyncEnumerable<IEvaluationResult> EvaluateCode(TaskRunner taskRunner, CodeEvaluationRequest request, CancellationToken token = default) {
    return taskRunner.Evaluate(request.Code, request.EvaluationModel, token);
}

IAsyncEnumerable<CodeGenerationResult> GenerateCode(TaskRunner taskRunner, CodeGenerationTask request, CancellationToken token = default) {
    return taskRunner.Generate(request, token);
}

IAsyncEnumerable<IEvaluationResult> FullPass(TaskRunner taskRunner, TaskSet request, CancellationToken token = default) {
    return taskRunner.Process(request, token);
}
