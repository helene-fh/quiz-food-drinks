namespace quiz_food_drinks.Entities;

public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string QuestionText { get; set; }

    public string Category { get; set; }
    
}