using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Controllers;


[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet]
    public async Task<object?> GetQuiz()
    { 
        return await _quizService.GetRandomQuiz();
    }

}

