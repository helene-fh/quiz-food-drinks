using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.Services;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Controllers;
[ApiController]
[Route("api/question_Trivia/[controller]")]
[ApiExplorerSettings(GroupName = "question_Trivia")]
[Produces("application/json")]

public class QuestionController : ControllerBase
{
   private readonly IQuestionService _questionService;
    
    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    /// <summary>
    /// Add a question
    /// </summary>
    /// <returns>A added question</returns>
    /// <remarks>
    /// **Sample request:**
    ///     
    ///```
    /// POST /Todo
    /// {
    ///     "questionText": "string",
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "category": "string"
    /// }
    /// ```
    /// </remarks>
    /// <response code="200">Success, added a new question</response>
    /// <response code="404">Error 404, try again later?</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> AddQuestion(QuestionCreateRequest question)
    {
        if (question is null)
        {
            return NotFound("Invalid input");
        }

        return Ok(await _questionService.AddQuestion(question));
    }

    /// <summary>
    /// Get all Questions!
    /// </summary>
    /// <returns>All Questions</returns>
    /// <remarks>
    /// **Sample request:**
    /// ```
    /// GET
    /// [
    /// {
    ///   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///   "questionText": "Some random question",
    ///   "category": "Some category"
    /// },
    /// {
    ///   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///   "questionText": "Some random question",
    ///   "category": "Some category"
    /// },
    /// ]
    /// ```
    /// </remarks>
    /// <response code="200">Get all Questions!</response>
    /// <response code="400">Bad request!</response>
    [HttpGet(Name ="GetAllQuestions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Question>> GetQuestions()
    {
        var question = await _questionService.AllQuestions();
        if (question!=null) {return Ok(question); }
        return BadRequest();
    }

    /// <summary>
    /// Get question by id
    /// </summary>
    /// <returns>A question</returns>
    /// <remarks>
    /// **Sample request:**
    ///   ```
    ///     GET
    ///     {
    ///   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///   "questionText": "Some random question",
    ///   "category": "Some category"
    ///     }
    ///```
    /// </remarks>
    /// <response code="200">You get the question</response>
    /// <response code="404">Invalid id</response>
    /// <param name="id">Question id(Guid) to found question</param>
    [HttpGet("{id}",Name ="GetQuestionById")]
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
    /// Get a random question
    /// </summary>
    /// <returns>Randomed question</returns>
    /// <remarks>
    /// **Sample request:**
    ///     
    /// ```
    /// GET
    /// {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "questionText": "Who counts as a sith-lord?",
    ///     "category": "Star Wars"
    /// }
    /// ```
    /// </remarks>
    /// <response code="200">Success, gets a random question</response>
    /// <response code="404">Failed, Not Found try again!</response>
    [HttpGet]
    [Route("api/[controller]/Random")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> GetRandomQuestion()
    {
        var question = await _questionService.GetRandomQuestion();
        if (question == null) { return NotFound("E404,Not Found"); }
        return Ok(question);

    }


    /// <summary>
    /// Update a question by id
    /// </summary>
    /// <returns>Updated question</returns>
    /// <remarks>
    /// **Sample request:**
    ///     
    ///   ```
    ///   PUT /Todo
    ///   {
    ///     "questionText": "string",
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "category": "string",
    ///     "questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    ///   }
    ///   ```
    ///</remarks>
    /// <response code="200">Success, updated question</response>
    /// <response code="400">Error 400, check inputs and id(Guid)</response>
    /// <param name="id">id(Guid) of the Question to update</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Question>> UpdateQuestion(QuestionUpdateRequest question, Guid id)
    {
        if (id != question.QuestionId)
        {
            return BadRequest("Invalid input");
        }

        await _questionService.GetQuestion(id);

        if (question.QuestionId == id)
        {
            await _questionService.UpdateQuestion(question);
        }
        return Ok(question);
    }

    

    /// <summary>
    /// Delete by id
    /// </summary>
    /// <returns>Deleted question</returns>
    /// <remarks>
    /// **Sample request:**
    ///```
    /// DELETE /Todo
    /// {
    /// "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    /// "questionText": "Who counts as a sith-lord?",
    /// "category": "Star Wars"
    /// }
    /// ```
    ///</remarks>
    /// <response code="200">Success, deleted the question</response>
    /// <response code="404">Invalid id</response>
    /// <param name="id">Question id(Guid) to delete question</param>
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