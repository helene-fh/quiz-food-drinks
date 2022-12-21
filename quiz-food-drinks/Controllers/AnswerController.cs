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
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {

        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService) {
            _answerService = answerService;
        }

        
        [HttpGet]
        public async Task<ActionResult<Question>> GetAnswers()
        {
            var answer = await _answerService.AllAnswers();
        
            return Ok(answer);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> Get(Guid id)
        {
            var answers = await _answerService.Get(id);
            return Ok(answers);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Answer>> AddAnswer(AnswerCreateRequest? answer)
        {
            if (answer is null)
            {
                return NotFound();
            }
            return Ok(await _answerService.AddAnswer(answer));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Answer>> Delete(Guid id)
        {
            
            var deleteAnswer = await _answerService.DeleteAnswer(id);
            if (deleteAnswer!=null) {
                return Ok(deleteAnswer);
            }
            return NotFound();

        }
    }
}

