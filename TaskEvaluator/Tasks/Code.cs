using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed record Code(Guid Guid, string Body, EntryPoint EntryPoint, ProgrammingLanguage Language);
