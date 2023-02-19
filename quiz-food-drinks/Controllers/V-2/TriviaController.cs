using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Interfaces.Repositories;

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
}
