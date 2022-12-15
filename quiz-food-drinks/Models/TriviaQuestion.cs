using System.Text.Json.Serialization;

namespace quiz_food_drinks.Models;

public class TriviaQuestion
{
    public string Category { get; set; } 
    
    public string id { get; set; }
    
    public string correctAnswer { get; set; }
    
    public List<string> incorrectAnswers { get; set; }
    
    public string question { get; set; }
    
    public List<string> tags { get; set; }
    
    public string type { get; set; }
    
    public string difficulty { get; set; }
    
    public List<object> regions { get; set; }
    
    public bool isNiche { get; set; }
}