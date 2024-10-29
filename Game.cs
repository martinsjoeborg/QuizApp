namespace QuizApp;
class Game
{
    private int _score = 0;
    private IEnumerable<Question> _questions;
    private Writer _writer;

    public List<string> storedScores = [];

    public Game(IEnumerable<Question> questions, Writer writer)
    {
        if (!questions.Any())
        {
            throw new ArgumentException("Questions can't be null or empty.");
        }

        _questions = questions;
        _writer = writer;
    }
    public void Start()
    {
        bool keepPlaying = true;

        while(keepPlaying)
        {
            _writer.InfoLine("\n🎉 Welcome to the Quiz Game! 🎉\n");
            foreach (var question in _questions)
            {
                AskQuestion(question);
            }

            if(_score == _questions.Count())
            {
                _writer.Success($"🎉 You got all right! 🎉\n");
            }
            else
            {
                _writer.Info($"Your final score is: {_score}/{_questions.Count()}\n");
            }
            
            StoreScores(_score, _questions.Count());
            Console.ResetColor();

            Console.WriteLine("");

            _score = 0;

            Console.WriteLine("👇 Your previous scores");
            showScores();


            while(true)
            {
                _writer.Info("Do you wanna play again? Type 'y' for yes and 'n' for no: ");
                string yesOrNo = Console.ReadLine()!;

                if(yesOrNo == "y")
                {
                    Console.Clear();
                    break;
                }
                else if(yesOrNo == "n")
                {
                    _writer.Info("Bye Bye 🖐🖐");
                    keepPlaying = false;
                    break;
                }
                else
                {
                    _writer.Info("Please enter 'y' or 'n' \n");
                    // continue;
                }
                
            }
        }
        
    }
    private void AskQuestion(Question question)
    {
        _writer.InfoLine($"🧩{question.QuestionText}");
        for (int i = 0; i < question.Options.Count; i++)
        {
            _writer.InfoLine($"     {i}: {question.Options[i]}");
        }
        _writer.InfoLine();
        Answer answer = GetAnswerFromUser(question.Options.Count);

        if (answer.SelectedOption == question.CorrectOption)
        {
            _writer.Success("Correct!\n");
            _score++;
        }
        else
        {
            _writer.Error($"Incorrect. The correct answer is: {question.Options[question.CorrectOption]}\n");
        }
        _writer.InfoLine($"You answered: {answer.SelectedOption}");
        Console.WriteLine("");
    }
    private Answer GetAnswerFromUser(int numberOfOptions)
    {
        int selectedOption;
        while (true)
        {
            _writer.Info("Select an option 👉 ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out selectedOption) && selectedOption >= 0 && selectedOption < numberOfOptions)
            {
                break;
            }
            _writer.WarningLine("Invalid option. Please try again.\n");
        }
        return new Answer(selectedOption);
    }

    public void StoreScores(int score, int totalScore)
    {
        string newScore = $"{score}/{totalScore}";
        storedScores.Add(newScore);
    }

    public void showScores()
    {
        var orderedScoreList = storedScores.OrderByDescending(s => s);

        foreach (var score in orderedScoreList)
        {
            if(score == "3/3")
            {
                _writer.SuccessLine(score);
                Console.ResetColor();
                continue;
            }
            Console.WriteLine(score);
        }
    }
}