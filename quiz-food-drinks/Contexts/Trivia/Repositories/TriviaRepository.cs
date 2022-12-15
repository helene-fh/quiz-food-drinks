using System.Text.Json;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Contexts.Trivia.Repositories;

public class TriviaRepository : ITriviaRepository
{
    public async Task<TriviaQuestion> GetTriviaQuestion()
    {
        var uri = "https://the-trivia-api.com/api/questions?categories=food_and_drink&limit=20&region=SE&difficulty=medium";

        using var client = new HttpClient();

        var response = await client.GetAsync(uri);

        var stream = await response.Content.ReadAsStreamAsync();

        var triviaQuestion = await JsonSerializer.DeserializeAsync<List<TriviaQuestion>>(stream);

       return triviaQuestion[0];
    }
}