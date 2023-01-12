using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace quiz_food_drinks.Controllers;

[ApiController]
[Route("api/question_Trivia/[controller]")]
[ApiExplorerSettings(GroupName = "question_Trivia")]
[Produces("application/json")]

public class TriviaController : ControllerBase
{
    private readonly ITriviaRepository _triviaRepository;

    public TriviaController(ITriviaRepository triviaRepository)
    {
        _triviaRepository = triviaRepository;
    }

    
    /*[HttpGet]
    public async Task<IActionResult> GetTrivia() 
    {
        var triviaQuestion = await _triviaRepository.GetTriviaQuiz();
        var answersList = new List<string>();
        Random rand = new Random();
        answersList.Add(triviaQuestion.CorrectAnswer);
        
        Console.WriteLine("TRIVIA API");
        Console.WriteLine(triviaQuestion.Question);
        
        triviaQuestion.IncorrectAnswers.ForEach(i => answersList.Add(i));
        var shuffledList = answersList.OrderBy(_ => rand.Next()).ToList();

        foreach (var answer in shuffledList)
        {
            if (answer == triviaQuestion.CorrectAnswer)
            {
                Console.WriteLine($"{answer}(correct)");
            }
            else
            {
                Console.WriteLine(answer);
                
            }
        }
        
        return Ok(triviaQuestion);
    }*/
}
