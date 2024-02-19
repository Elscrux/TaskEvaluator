using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskEvaluator.Cli;
using TaskEvaluator.Modules;
using TaskEvaluator.Sink.PostgreSQL;
using TaskEvaluator.SonarQube;
using TaskEvaluator.Specification.CSharp;
using TaskEvaluator.Tasks;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddLogging();
builder.Services.AddHttpClient();

builder.Configuration.AddUserSecrets<TaskRunner>();

builder.Services.AddTaskEvaluator(builder.Configuration)
    .Language.Add<CSharpRegistration>()
    .Evaluator.AddSonarQube()
    .Sink.AddLogger()
    .Sink.AddPostgreSQL();

builder.Services.AddTransient<IHostedService, BatchRunner>();

var app = builder.Build();

app.Run();
