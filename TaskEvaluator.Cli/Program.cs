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

builder.Services.AddTaskEvaluator(builder.Configuration);
builder.Services.AddSonarQube(builder.Configuration);
builder.Services.AddPostgreSQL(builder.Configuration);
builder.Services.AddLanguage<CSharpRegistration>();

builder.Services.AddTransient<IHostedService, BatchRunner>();

var app = builder.Build();

app.Run();
