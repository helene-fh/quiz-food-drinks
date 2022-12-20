using Microsoft.AspNetCore.Http.HttpResults;
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
    
 
    // Hämta fråga från trivia
    // Hämta fråga från databas (SKA HÄTA ETT HELT QUIZ FRÅN DATABAS)
    // Slumpa vilken som ska exponeras utåt
    
    // Om fråga kommer från trivia konvertera till QuizModel
    // Addera quizen till databasen
    public async Task<object?> GetRandomQuiz()
    {
        Random random = new Random();
        
        var trivia = await _triviaRepository.GetTriviaQuiz();
        var question = await _questionService.GetRandomQuestion();

        var quizList = new List<object?>();
        
        quizList.Add(trivia);
        quizList.Add(question);
        
        var randomQuiz = quizList.ElementAt(random.Next(0, quizList.Count));

        return randomQuiz;
    }
    
    
    
}