using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Models;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Interfaces.Services;

public interface IQuizService
{
    //Task<Question> GetQuiz();
    Task<QuizModel?> GetRandomQuiz();
    Task<AnswerBase> getTrue(int input);
}