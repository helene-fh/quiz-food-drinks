namespace quiz_food_drinks.Entities;

public class Answer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool IsCorrectAnswer { get; set; }
}