using WebApplication1;
using Task = System.Threading.Tasks.Task;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddTransient<IEvaluatorProvider, EvaluatorProvider>();
builder.Services.AddTransient<LocalTaskProvider, LocalTaskProvider>();
builder.Services.AddKeyedTransient<IRuntimeFactory, CSharpRuntimeFactory>(ProgrammingLanguage.CSharp);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[] {
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/evaluate", EvaluateTask)
	.WithName("Evaluate")
	.WithOpenApi();

app.Run();


async Task EvaluateTask(HttpContext context, TaskRunner taskRunner) {
	var taskEvaluationRequest = await context.Request.ReadFromJsonAsync<TaskEvaluationModel>();
	if (taskEvaluationRequest is null) return;

	taskRunner.Run(taskEvaluationRequest);
}
