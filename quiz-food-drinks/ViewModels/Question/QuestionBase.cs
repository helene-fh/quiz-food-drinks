using quiz_food_drinks.Interfaces.Entitites;

namespace quiz_food_drinks.ViewModels.Question.cs;

public abstract class QuestionBase : IQuestion
{
    public string QuestionString { get; set; } = null!;
    public Guid Id { get; set; }
    public string Category { get; set; } = null!;

    protected QuestionBase()
    {
  
    }
    protected QuestionBase(string questionString, string category)
    {

    }
}