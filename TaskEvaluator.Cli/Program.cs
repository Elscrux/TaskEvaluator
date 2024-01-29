using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskEvaluator.Cli;
using TaskEvaluator.Modules;
using TaskEvaluator.Runtime.Implementation.CSharp;
using TaskEvaluator.SonarQube;
using TaskEvaluator.Tasks;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddLogging();
builder.Services.AddHttpClient();

builder.Configuration.AddUserSecrets<TaskRunner>();

builder.Services.AddTaskEvaluator();
builder.Services.AddCSharp();
builder.Services.AddSonarQube();

builder.Services.AddTransient<IHostedService, BatchRunner>();

var app = builder.Build();

app.Run();
