using quiz_food_drinks.Entities;

namespace quiz_food_drinks.Models;

public class QuizModel
{
    public Guid Id { get; set; }
    
    public Question Question { get; set; }
    
    public List<Answer> Answers { get; set; }
    
    public Category Category { get; set; }
}