using quiz_food_drinks.Interfaces.Entitites;

namespace quiz_food_drinks.ViewModels.Question.cs;

public abstract class QuestionBase : IQuestion
{
    public string QuestionText { get; set; }
    public string Category { get; set; }

}