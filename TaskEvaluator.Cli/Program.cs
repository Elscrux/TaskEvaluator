using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskEvaluator.Cli;
using TaskEvaluator.Modules;
using TaskEvaluator.Runtime.Implementation.CSharp;
using TaskEvaluator.Tasks;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddLogging();
builder.Services.AddHttpClient();

builder.Services.AddTaskEvaluator();
builder.Services.AddCSharp();

builder.Services.AddTransient<IHostedService, BatchRunner>();
builder.Services.AddTransient<ITaskLoader>(_ => new LocalTaskLoader(@"E:\TES\Skyrim\dev\TaskEvaluator\Samples")); // todo redirect based on input argument

var app = builder.Build();

app.Run();
