using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Controllers;


[ApiController]
[Route("api/quiz/[controller]")]
[ApiExplorerSettings(GroupName = "quiz")]
[Produces("application/json")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly IAnswerService _answerService;
    public QuizController(IQuizService quizService, IAnswerService answerService)
    {
        _quizService = quizService;
        _answerService = answerService;
    }

    /// <summary>
    /// Get random question from Triva
    /// </summary>
    /// <returns>Random question</returns>
    /// <remarks>
    /// **Sample request:** 
    /// ```
    /// GET
    ///  {
    ///     "id": "00000000-624a-b0de-348a-461bfc6706a1",
    ///      "category": "Food &amp; Drink",
    ///     "question": "Which bean is used to produce a tin of baked beans?",
    ///     "answers": [
    ///     "1.Butter",
    ///     "2.Canelini",
    ///     "3.Berlotti",
    ///     "4.Haricot "
    ///     ]
    ///  }    
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

    /// <summary>
    /// Check if your guessed answer is correct
    /// </summary>
    /// <param name="answerInputId">Enter one of the listed answers number (int)</param>
    /// <returns>String line with either right or wrong answer!</returns>
    /// <remarks>
    /// **Sample request:**
    /// ```
    /// GET
    /// {
    /// "questionId": "00000000-0000-0000-0000-000000000000",
    /// "answerText": "Corn",
    /// "isCorrectAnswer": false
    /// }
    /// ```
    /// </remarks>
    /// /// <response code="200">Success,you get a correct or incorrect answer!</response>
    /// <response code="404">Get a Quiz before answering!</response>
    [HttpGet("{answerInputId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AnswerBase>> Check(int answerInputId) {

        var checker = await _quizService.GetTrue(answerInputId);
        if (checker == null) { return NotFound("Get a Quiz first!"); }

        return Ok(checker);   
    }
}

