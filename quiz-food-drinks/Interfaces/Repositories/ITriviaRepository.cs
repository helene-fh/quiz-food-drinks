using quiz_food_drinks.Models;

namespace quiz_food_drinks.Interfaces.Repositories;

public interface ITriviaRepository
{
   Task<List<TriviaModel>?> GetTriviaQuiz();
}