using System.Text.Json.Serialization;
using TaskEvaluator.Api.Api;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Runtime;
using TaskEvaluator.Runtime.Implementation.CSharp;
using TaskEvaluator.Tasks;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SchemaFilter<ExampleSharpFilter>());
builder.Services.AddLogging();
builder.Services.AddHttpClient();

builder.Services.AddTransient<IEvaluatorProvider, EvaluatorProvider>();
builder.Services.AddTransient<UnitTestEvaluator>();
builder.Services.AddTransient<LocalTaskProvider, LocalTaskProvider>();
builder.Services.AddSingleton<LanguageFactory>();
builder.Services.AddTransient<TaskRunner>();
builder.Services.AddCSharp();
builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/evaluate", EvaluateTask)
    .WithName("Evaluate")
    .WithOpenApi();

app.Run();

IAsyncEnumerable<IEvaluationResult> EvaluateTask(TaskRunner taskRunner, TaskEvaluationModel model, CancellationToken token = default) {
    return taskRunner.Run(model, token);
}
