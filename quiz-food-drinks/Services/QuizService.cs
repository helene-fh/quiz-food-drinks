using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuestionService _questionService;
    private readonly ITriviaRepository _triviaRepository;

    public QuizService(IQuizRepository quizRepository, IQuestionService questionService, ITriviaRepository triviaRepository)
    {
        _quizRepository = quizRepository;
        _questionService = questionService;
        _triviaRepository = triviaRepository;
    }
    
 
    /*public Task<IActionResult> GetRandomQuiz()
    {
        Random random = new Random();
        
    }*/
}