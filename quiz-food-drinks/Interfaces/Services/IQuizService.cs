using quiz_food_drinks.Models;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Interfaces.Services;

public interface IQuizService
{
    Task<QuizModel?> GetRandomQuiz();
    Task<AnswerBase?> GetTrue(int answerInputId);
}