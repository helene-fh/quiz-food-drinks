using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.ViewModels.Answer.cs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace quiz_food_drinks.Controllers
{
    [ApiController]
    [Route("api/answer/[controller]")]
    [ApiExplorerSettings(GroupName = "answer")]
    [Produces("application/json")]
    public class AnswerController : ControllerBase
    {

        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService) {
            _answerService = answerService;
        }


        /// <summary>
        /// Add a new answer to a Question(id)
        /// </summary>
        /// <returns>A new added Answer to a Question.</returns>
        /// <remarks>
        ///**Sample request:**
        /// ```
        /// POST /Todo
        /// {
        ///     "questionId": "Enter an id from a Question!",
        ///     "answerText": "It´s Superman",
        ///     "isCorrectAnswer": true
        /// }
        /// ```
        ///</remarks>
        /// <response code="200">Success, added a new answer to a question by id(Guid)</response>
        /// <response code="404">Error 404, invalid id(Guid)</response>
        [HttpPost(Name = "AddAnAnswer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Answer>> AddAnswer(AnswerCreateRequest? answer)
        {
            if (answer is null)
            {
                return NotFound("Invalid id");
            }
            return Ok(await _answerService.AddAnswer(answer));
        }



        /// <summary>
        /// Get all answers.
        /// </summary>
        /// <returns>A list of all the answers</returns>
        /// <remarks>
        /// **Sample request:**
        ///```
        ///GET
        ///[
        /// {
        ///"questionId": "6658a09f-5532-4e28-951f-637dfe5f66c1",
        /// "answerText": "Yolo",
        /// "isCorrectAnswer": false,
        /// "id": "333dfc00-9a56-42b9-b1fa-678e0e50f3b5"
        /// },
        /// {
        ///"questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        /// "answerText": "Me",
        /// "isCorrectAnswer": true,
        /// "id": "333dfc00-9a56-42b9-b1fa-678e0e50f3b5"
        /// }
        ///]
        ///```
        /// </remarks>
        /// <response code="200">Success, get list of all answers</response>
        /// <response code="400">Error 400, Bad request!</response>
        [HttpGet(Name ="GetAllAnswers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Answer>> GetAnswers()
        {
            var answer = await _answerService.AllAnswers();
            if (answer==null) { return BadRequest(); }
            return Ok(answer);
        }

        /// <summary>
        /// Get Answer from id.
        /// </summary>
        /// <param name="id">Answer id(Guid) to found answer!</param>
        /// <returns>An Answer</returns>
        /// <remarks>
        ///   **Sample request:**
        ///    ``` {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "questionId": "6B29FC40-CA47-1067-B31D-00DD010662DA",
        ///         "answerText": "Leia",
        ///         "isCorrectAnswer": false
        ///     }
        ///     ```
        /// </remarks>
        /// <response code="200">Returns an answer</response>
        /// <response code="404">Invalid id</response>
        [HttpGet("{id}")] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
       
        public async Task<ActionResult<Answer>> Get(Guid id)
        {
            var answers = await _answerService.Get(id);
            if (answers is null) { return NotFound("Invalid id"); }
            return Ok(answers);
        }

        

        /// <summary>
        /// Update an answer by id
        /// </summary>
        /// <param name="answer">Updated answer example</param>
        /// <param name="id">Answer id(Guid) to update an answer!</param>
        /// <returns>An updated answer</returns>
        /// <remarks>
        ///
        /// **Sample request:**
        /// ```  
        ///    {
        ///       "questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///       "answerText": "new updated text",
        ///       "isCorrectAnswer": new bool update,
        ///       "answerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///   }
        ///   ```
        /// </remarks>
        /// <response code="200">Success, answer was updated</response>
        /// <response code="400">Error 400,check inputs and id(Guid)</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Answer>>Put(AnswerEditRequest answer, Guid id)
        {
            if (id!=answer.AnswerId) {
                return BadRequest();
            }
            await _answerService.Get(id);
            if (answer.AnswerId==id) {
                await _answerService.EditAnswer(answer);
            }
            return Ok(answer);

        }

        /// <summary>
        /// Delete an answer
        /// </summary>
        /// <param name="id">Answer id(Guid) to be deleted!</param>
        /// <returns>deleted answer</returns>
        /// <remarks>
        ///**Sample request:**
        ///```{
        ///   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///   "questionId": "6B29FC40-CA47-1067-B31D-00DD010662DA",
        ///   "answerText": "Luke",
        ///   "isCorrectAnswer": false
        /// }``
    ///
    /// </remarks>
    /// <response code="200">Success, deleted answer</response>
    /// <response code="404">Invalid id</response>
    [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Answer>> Delete(Guid id)
        {
            
            var deleteAnswer = await _answerService.DeleteAnswer(id);
            if (deleteAnswer!=null) {
                return Ok(deleteAnswer);
            }
            return NotFound("Check the id");

        }
    }
}

