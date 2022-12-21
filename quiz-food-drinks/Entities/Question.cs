using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Entities;

public class Question : BaseEntity, IQuestion
{
    public string QuestionText { get; set; } = null!;
    public string Category { get; set; } = null!;

    public Question()
    {
    }

    public Question(string questionText, string category)
    {
        QuestionText = questionText;
        Category = category;
    }

}