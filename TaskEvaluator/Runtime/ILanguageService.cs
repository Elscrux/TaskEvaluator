﻿using TaskEvaluator.Language;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public interface ILanguageService {
    ProgrammingLanguage Language { get; }
    string FileExtension { get; }
    string LineCommentSymbol { get; }
    string ProgramFileName => $"Program{FileExtension}";

    void CreateWorkingDirectory(string workingDirectory, Code code);
    void CreateTestDirectory(string testDirectory, Code code);
}