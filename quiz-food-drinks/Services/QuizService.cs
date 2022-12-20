using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.ViewModels.Question.cs;

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

    
    


    // Om fråga kommer från trivia konvertera till QuizModel

    public async Task<object?> GetRandomQuiz()
    {
        Random random = new Random();
        
   
        var trivia = await GetSingleTrivia();
        var question = await GetQuestionFromDb();

        //var triviaId = trivia.Id;
       // var questionId = question.Id;
       
       
       

       // LINQ som går in Questions och ser om triviaId finns
        
        //Addera question i databas 
        //var newQuestion = new QuestionCreateRequest();
        //await _questionService.AddQuestion(newQuestion);

        
        // En lista för random  bara 
        var quizList = new List<object?>();
        
        quizList.Add(trivia);
        quizList.Add(question);
        
        // Slumpa vilken som ska exponeras utåt
        var randomQuiz = quizList.ElementAt(random.Next(0, quizList.Count));

        return randomQuiz;
    }

    public async Task<bool>? CheckTriviaQuestionIdInDb(Guid id)
    {
        return await _questionService.QuestionExists(id);
    }
    
    // Konverterar triviaModel till quizModel
    public async Task<QuizModel> GetSingleTrivia()
    {
        var response = await _triviaRepository.GetTriviaQuiz();
        QuizModel responseQuiz = null;
        
        foreach (var quiz in response)
        {
            responseQuiz = new QuizModel(quiz.Category, quiz.Id, quiz.Question);
        }
        
        return responseQuiz;
    }

    // Konverterar question till quizmodel, Ska bli quiz 
    public async Task<QuizModel> GetQuestionFromDb()
    {
        var response = await _questionService.AllQuestions();
        QuizModel responseQuiz = null;

        foreach (var quiz in response)
        {
            responseQuiz = new QuizModel(quiz.Category, quiz.Id.ToString(), quiz.QuestionText);
        }

        return responseQuiz;
    }

}