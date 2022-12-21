using System.Text.Json;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Contexts.Trivia.Repositories;

public class TriviaRepository : ITriviaRepository
{
    public async Task<List<TriviaModel>?> GetTriviaQuiz()
    {
        var uri = "https://the-trivia-api.com/api/questions?categories=food_and_drink&limit=1&region=SE&difficulty=medium";

        using var client = new HttpClient();

        var response = await client.GetAsync(uri);
        var stream = await response.Content.ReadAsStreamAsync();

        var triviaQuiz = await JsonSerializer.DeserializeAsync<List<TriviaModel>>(stream);

       return triviaQuiz;
    }
}