namespace quiz_food_drinks.Models;

public class QuizModel 
{
    public Guid Id { get; set; }
    
    public string Category { get; set; } = null!;

    public string Question { get; set; } = null!;

    public List<string> Answers { get; set; } = null!;
    
    public QuizModel()
    {
    }
}