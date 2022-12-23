using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Controllers;


[ApiController]
[Route("api/[controller]")]
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
    ///     **Sample request:**
    ///
    /// ```
    /// {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "questionText": "Who counts as a sith-lord?",
    ///     "category": "Star Wars"
    /// }
    /// ```
    /// </remarks>
    /// <response code="200">You get randomed question</response>
    /// <response code="400">Bad url?</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<object?> GetQuiz()
    {
        var triviaData = _quizService.GetRandomQuiz();
        if (triviaData==null) { return BadRequest("Try again later"); }
        return Ok(await triviaData);
    }

}

