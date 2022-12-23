using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;
using quiz_food_drinks.ViewModels;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Controllers;


[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly IAnswerService _answerService;

    public QuizController(IQuizService quizService, IAnswerService answerService)
    {
        _quizService = quizService;
        _answerService = answerService;
    }

    [HttpGet]
    public async Task<object?> GetQuiz()
    {
        return Ok(await _quizService.GetRandomQuiz());
    }
}

