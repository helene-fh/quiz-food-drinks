using System.ComponentModel;
using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Entities;

public class Question : BaseEntity, IQuestion
{
    [DefaultValue("Who counts as a sith-lord?")]
    public string QuestionText { get; set; } = null!;
    [DefaultValue("Star Wars")]
    public string Category { get; set; } = null!;

    public Question()
    {
    }

}
