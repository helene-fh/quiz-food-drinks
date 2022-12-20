namespace quiz_food_drinks.ViewModels.QuizGame;

public class QuizGame
{
    public Guid Id { get; set; }
    
    public string Category { get; set; }

    public string QuestionText { get; set; }

    public List<string> Answers { get; set; }
    
}