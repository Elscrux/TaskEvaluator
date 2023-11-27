using System.Text.Json.Serialization;
using WebApplication1;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SchemaFilter<ExampleSharpFilter>();
    // options.includ
    // options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "YourApiDocumentation.xml"));
});
builder.Services.AddLogging();


builder.Services.AddTransient<IEvaluatorProvider, EvaluatorProvider>();
builder.Services.AddTransient<UnitTestEvaluator, UnitTestEvaluator>();
builder.Services.AddTransient<LocalTaskProvider, LocalTaskProvider>();
builder.Services.AddTransient<RuntimeService, RuntimeService>();
builder.Services.AddTransient<TaskRunner, TaskRunner>();
builder.Services.AddKeyedTransient<IRuntimeFactory, CSharpRuntimeFactory>(ProgrammingLanguage.CSharp);
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


List<IEvaluationResult> EvaluateTask(HttpContext context, TaskRunner taskRunner, TaskEvaluationModel model) {
    return taskRunner.Run(model).ToList();
}