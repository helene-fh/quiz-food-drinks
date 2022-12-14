using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Controllers;
[ApiController]
[Route("api/[controller]")]

public class QuestionController : ControllerBase
{
   private readonly IQuestionService _questionService;
    
    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet]
    public async Task<ActionResult<Question>> GetQuestions()
    {
        var question = await _questionService.AllQuestions();
        
        return Ok(question);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestion(Guid id)
    {
        var question = await _questionService.GetQuestion(id);
        
        if (question is null)
        {
            return NotFound();
        }
        return Ok(question);
    }

    [HttpPost]
    public async Task <ActionResult<Question>> AddQuestion(QuestionCreateRequest question)
    {
        if (question is null)
        {
            return NotFound();
        }

        return Ok(await _questionService.AddQuestion(question));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Question>> UpdateQuestion(QuestionUpdateRequest question, Guid id)
    {
        if (id != question.QuestionId)
        {
            return BadRequest();
        }

        await _questionService.GetQuestion(id);
        
        if (question.QuestionId == id)
        {
            await _questionService.UpdateQuestion(question);
        }
        return Ok(question);
    }

    [HttpGet]
    [Route("api/[controller]/Random")]
    public async Task<ActionResult<Question>> GetRandomQuestion()
    {
        var question = await _questionService.GetRandomQuestion();
        
        return Ok(question);

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Question>> DeleteQuestion(Guid id)
    {
        var question = await _questionService.DeleteQuestion(id);

        if (question is null)
        {
            return NotFound();
        }

        return Ok(question);
    }

}