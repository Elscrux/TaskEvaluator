using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskEvaluator.Cli;
using TaskEvaluator.Modules;
using TaskEvaluator.Runtime.Implementation.CSharp;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddLogging();
builder.Services.AddHttpClient();

builder.Services.AddTaskEvaluator();
builder.Services.AddCSharp();

builder.Services.AddTransient<IHostedService, BatchRunner>();

var app = builder.Build();

app.Run();
