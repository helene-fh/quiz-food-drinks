using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace quiz_food_drinks.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]

public class TriviaController : ControllerBase
{
    private readonly ITriviaRepository _triviaRepository;

    public TriviaController(ITriviaRepository triviaRepository)
    {
        _triviaRepository = triviaRepository;
    }



    [HttpGet]
    public async Task<IActionResult> Get() 
    {
        var triviaQuestion = await _triviaRepository.GetTriviaQuestion();
        var answersList = new List<string>();
        Random rand = new Random();
        answersList.Add(triviaQuestion.correctAnswer);
        
        Console.WriteLine("TRIVIA API");
        Console.WriteLine(triviaQuestion.question);
        
        triviaQuestion.incorrectAnswers.ForEach(i => answersList.Add(i));
        var shuffledList = answersList.OrderBy(_ => rand.Next()).ToList();

        foreach (var answer in shuffledList)
        {
            if (answer == triviaQuestion.correctAnswer)
            {
                Console.WriteLine($"{answer}(correct)");
            }
            else
            {
                Console.WriteLine(answer);
            }
        }
        
        return Ok(triviaQuestion);
    }
}
