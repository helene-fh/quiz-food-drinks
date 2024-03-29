using System.ComponentModel;
using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Entities;

public class Question : BaseEntity, IQuestion
{

    [DefaultValue("Which familiar carbonated soft drink contains quinine?")]
    public string QuestionString { get; set; } = null!;
    [DefaultValue("Food & Drink")]
    public string Category { get; set; } = null!;


    public Question()
    {
    }

}
