using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Controllers;


[ApiController]
[Route("api/quiz/[controller]")]
[ApiExplorerSettings(GroupName = "quiz")]
[Produces("application/json")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    /// <summary>
    /// Get random question from Triva
    /// </summary>
    /// <returns>Random question</returns>
    /// <remarks>
    /// **Sample request:** 
    /// ```
    ///     {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "questionText": "Who counts as a sith-lord?",
    ///     "category": "Star Wars"
    ///     }
    /// ```
    /// </remarks>
    /// <response code="200">Success,you got a randomed question</response>
    /// <response code="204">Content missing</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    
    public async Task<object?> GetQuiz()
    {
        var triviaData = _quizService.GetRandomQuiz();
        if (triviaData == null) { return NoContent(); }
        return Ok(await triviaData);
    }


    [HttpGet("{input}")]
    public Task<string> Check(int input) {

        var checker = _quizService.getTrue(input);
        return checker;

    }

}

