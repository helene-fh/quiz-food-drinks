using System.Text.Json.Serialization;

namespace quiz_food_drinks.Models;

public class TriviaModel
{
    [JsonPropertyName("category")]
    public string Category { get; set; } = null!;

    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("correctAnswer")]
    public string CorrectAnswer { get; set; } = null!;

    [JsonPropertyName("incorrectAnswers")]
    public List<string> IncorrectAnswers { get; set; } = null!;

    [JsonPropertyName("question")]
    public string Question { get; set; } = null!;
}