using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.Services;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Controllers;
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class QuestionController : ControllerBase
{
   private readonly IQuestionService _questionService;
    
    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    /// <summary>
    /// Get a list of all Questions
    /// </summary>
    /// <returns>List of Questions</returns>
    /// <remarks>
    ///     **Sample request:**
    ///     
    ///  ```
    ///  [{
    ///     "questionText": "How is steak tartare cooked?",
    ///        "category": "Food & Drink",
    ///       "id": "00000000-622a-1c3b-7cc5-9eab6f9515ad"
    ///       },]
    ///       ```
    /// </remarks>
    /// <response code="200">You get list of questions</response>
    /// <response code="400">Something went wrong, try later</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Question>> GetQuestions()
    {
        var question = await _questionService.AllQuestions();
        if (question!=null) {return Ok(question); }
        return BadRequest("400, try again later");
    }

    /// <summary>
    /// Get question by id
    /// </summary>
    /// <returns>A question</returns>
    /// <remarks>
    ///     **Sample request:**
    ///   ```  
    ///     {
    ///   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///   "questionText": "Who counts as a sith-lord?",
    ///   "category": "Star Wars"
    ///      }
    ///```
    /// </remarks>
    /// <response code="200">You get the question</response>
    /// <response code="404">Invalid id</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> GetQuestion(Guid id)
    {
        var question = await _questionService.GetQuestion(id);
        
        if (question is null)
        {
            return NotFound("Invalid id");
        }
        return Ok(question);
    }


    /// <summary>
    /// Add a question
    /// </summary>
    /// <returns>A added question</returns>
    /// <remarks>
    ///
    ///     **Sample request:**
    ///     
    ///```
    /// {
    ///     "questionText": "string",
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "category": "string"
    /// }
    /// ```
/// </remarks>
/// <response code="200">Added a new question</response>
/// <response code="404">Something went badly, try again?</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task <ActionResult<Question>> AddQuestion(QuestionCreateRequest question)
    {
        if (question is null)
        {
            return NotFound("Check inputs!");
        }

        return Ok(await _questionService.AddQuestion(question));
    }

    /// <summary>
    /// Update a question by id
    /// </summary>
    /// <returns>Updated question</returns>
    /// <remarks>
    ///     **Sample request:**
    ///     
    ///   ```
    ///   {
    ///     "questionText": "string",
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "category": "string",
    ///     "questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    ///     }
    ///     ```
    ///
    /// </remarks>
    /// <response code="200">Updated question</response>
    /// <response code="400">Something went wrong</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Question>> UpdateQuestion(QuestionUpdateRequest question, Guid id)
    {
        if (id != question.QuestionId)
        {
            return BadRequest("Check inputs!");
        }

        await _questionService.GetQuestion(id);
        
        if (question.QuestionId == id)
        {
            await _questionService.UpdateQuestion(question);
        }
        return Ok(question);
    }

    /// <summary>
    /// Get a random question
    /// </summary>
    /// <returns>Randomed question</returns>
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
    /// <response code="200">Gets a random question</response>
    /// <response code="404">Dont get a random question</response>
    [HttpGet]
    [Route("api/[controller]/Random")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> GetRandomQuestion()
    {
        var question = await _questionService.GetRandomQuestion();
        if (question==null) { return NotFound("Try again"); }
        return Ok(question);

    }

    /// <summary>
    /// Delete by id
    /// </summary>
    /// <returns>Deleted question</returns>
    /// <remarks>
    ///     **Sample request:**
    ///```
    /// {
    /// "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    /// "questionText": "Who counts as a sith-lord?",
    /// "category": "Star Wars"
    ///}``
    ///
    /// </remarks>
    /// <response code="200">Deleted question</response>
    /// <response code="404">Invalid id</response>
[HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> DeleteQuestion(Guid id)
    {
        var question = await _questionService.DeleteQuestion(id);

        if (question is null)
        {
            return NotFound("Invalid id");
        }

        return Ok(question);
    }

}