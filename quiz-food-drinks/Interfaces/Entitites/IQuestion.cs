using System.ComponentModel.DataAnnotations;

namespace quiz_food_drinks.Interfaces.Entitites;

public interface IQuestion
{
    [Required]
    public string QuestionString { get; set; }
    
    [Required]
    public string Category { get; set; }
}