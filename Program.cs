namespace QuizApp;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var writer = new Writer();
            string questionFileName = args.Length == 1 ? args[0] : "questions.json";
            var questionLoader = new JsonQuestionLoader(questionFileName);
            var game = new Game(questionLoader.Questions, writer);
            game.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Sorry, unexpected error: {ex.Message}");
        }
        Console.WriteLine("");
    }
}