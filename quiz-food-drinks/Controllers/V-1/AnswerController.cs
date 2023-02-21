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
        /// Get all answers
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
        /// Get answers by question id
        /// </summary>
        /// <param name="id">Question id(Guid) to find relative answers!</param>
        /// <returns>A list of answers</returns>
        /// <remarks>
        ///   **Sample request:**
        ///    ```
        ///         GET
        ///         [
        ///         {
        ///         "questionId": "00000000-6271-6477-d5d4-87fbab6b795a",
        ///          "answerText": "Italy",
        ///          "isCorrectAnswer": true,
        ///         "id": "c670a7ff-1af2-454b-86ab-c955318938d7"
        ///         },
        ///         {
        ///          "questionId": "00000000-6271-6477-d5d4-87fbab6b795a",
        ///          "answerText": "Ghana",
        ///          "isCorrectAnswer": false,
        ///          "id": "dcc475e6-b400-4d71-9017-3b11e7bd1cf1"
        ///         },
        ///         {
        ///          "questionId": "00000000-6271-6477-d5d4-87fbab6b795a",
        ///          "answerText": "Panama",
        ///          "isCorrectAnswer": false,
        ///          "id": "3f1cc1b7-c6bd-454e-9b4c-32ba4e066189"
        ///          },
        ///          {
        ///          "questionId": "00000000-6271-6477-d5d4-87fbab6b795a",
        ///          "answerText": "Nepal",
        ///          "isCorrectAnswer": false,
        ///          "id": "075660aa-f370-47a2-b15d-27de60eb0641"
        ///          }
        ///         ]
        ///     ```
        /// </remarks>
        /// <response code="200">Success! Returns a list of answer</response>
        /// <response code="404">Error 404! Invalid id</response>
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
        ///     PUT /Todo
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
        public async Task<ActionResult<Answer>>Put(AnswerUpdateRequest answer, Guid id)
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
        /// Delete an answer by id
        /// </summary>
        /// <param name="id">Answer id(Guid) to be deleted!</param>
        /// <returns>deleted answer</returns>
        /// <remarks>
        ///**Sample request:**
        ///```
        ///  DELETE /Todo
        /// {
        ///   "questionId": "00000000-6271-6477-d5d4-87fbab6b795a",
        ///   "answerText": "Nepal",
        ///   "isCorrectAnswer": false,
        ///   "id": "075660aa-f370-47a2-b15d-27de60eb0641"
        /// }
        /// ```
        ///
        /// </remarks>
        /// <response code="200">Success, deleted answer</response>
        /// <response code="404">Error 404,Invalid id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Answer>> Delete(Guid id)
        {    
            var deleteAnswer = await _answerService.DeleteAnswer(id);
            if (deleteAnswer!=null) {
                return Ok(deleteAnswer);
            }

            return NotFound("Invalid id");
        }
    }
}

