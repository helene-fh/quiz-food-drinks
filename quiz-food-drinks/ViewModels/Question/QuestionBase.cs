using quiz_food_drinks.Interfaces.Entitites;

namespace quiz_food_drinks.ViewModels.Question.cs;

public abstract class QuestionBase : IQuestion
{
    public string QuestionText { get; set; } = null!;
    public Guid Id { get; set; }
    public string Category { get; set; } = null!;

    public QuestionBase(string questionString, Guid id, string category)
    {
        QuestionText = questionString;
        Id = id;
        Category = category;
    }

    protected QuestionBase()
    {
  
    }

    protected QuestionBase(string questionString, string category)
    {

    }
}