using TaskEvaluator.Language;
namespace TaskEvaluator.Task;

public sealed record Code(Guid Guid, string Body, EntryPoint EntryPoint, ProgrammingLanguage Language);
