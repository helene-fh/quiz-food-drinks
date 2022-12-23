using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Entities;

public class Question : BaseEntity, IQuestion
{
    public string QuestionString { get; set; }
    public string Category { get; set; }

    public Question()
    {
    }
    
}