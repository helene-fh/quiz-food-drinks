using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Interfaces.Services;

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

    /*[HttpGet]
    public async Task<IActionResult> GetQuiz()
    {
        return await _quizService.GetRandomQuiz();
    }*/

}

