using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Interfaces.Services;

public interface IQuizService
{
    //Task<Question> GetQuiz();
    Task<QuizModel?> GetRandomQuiz();
    Task<string>? getTrue(int input);
}