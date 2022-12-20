using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_answerService.GetAnswer());
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        //[Route("AddAnswer")]
        public IActionResult AddAnswer(AnswerCreateRequest answer)
        {
            return Ok(_answerService.AddAnswer(answer));
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        //[Route("Delete")]
        public void Delete(Guid id)
        {
            _answerService.DeleteAnswer(id);
        }
    }
}

