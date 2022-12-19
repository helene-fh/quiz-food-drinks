using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.Services;
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

    /*[HttpGet]
    public IActionResult GetQuestions()
    {
        return Ok(_questionService.Get());
    }*/
    

    [HttpPost]
    public IActionResult AddQuestion(QuestionCreateRequest question)
    {
        return Ok( _questionService.AddQuestion(question));
    }


}