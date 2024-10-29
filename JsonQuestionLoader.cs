using System.Text.Json;
namespace QuizApp;

class JsonQuestionLoader
{
    public IEnumerable<Question> Questions { get; }
    public JsonQuestionLoader(string fileName)
    {
        Questions = LoadQuestions(fileName);
    }
    private static IEnumerable<Question> LoadQuestions(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var questions = JsonSerializer.Deserialize<List<Question>>(json);
        if (questions == null)
        {
            throw new Exception($"Couldn't load the file {filePath}");
        }

        return questions;
    }
}