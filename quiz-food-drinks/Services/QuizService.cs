using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.ViewModels.Answer.cs;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuestionService _questionService;
    private readonly IAnswerService _answerService;
    private readonly ITriviaRepository _triviaRepository;
    private static List<Answer>? shuffledAnswersList;
    private QuizModel? trivia;
    private QuizModel quiz;

    public QuizService(IQuizRepository quizRepository, IQuestionService questionService, IAnswerService answerService, ITriviaRepository triviaRepository)
    {
        _quizRepository = quizRepository;
        _questionService = questionService;
        _answerService = answerService;
        _triviaRepository = triviaRepository;
    }
    

    public async Task<QuizModel?> GetRandomQuiz()
    {
        Random random = new Random();
        var quizList = new List<object?>
        {
            true,
            false
        };
        var randomsQuiz = quizList.ElementAt(random.Next(0, quizList.Count));
        if (randomsQuiz.Equals(true))
        {
            trivia = await GetSingleTrivia();
            return trivia as QuizModel;
        }
        else
        {
            quiz = await GetQuizFromDb();
            return quiz as QuizModel;
        }

       // trivia = await GetSingleTrivia();
       // quiz = await GetQuizFromDb();
        //var triviaId = trivia.Id;
        // var questionId = question.Id;

        // LINQ som går in Questions och ser om triviaId finns

        //Addera question i databas 
        //var newQuestion = new QuestionCreateRequest();
        //await _questionService.AddQuestion(newQuestion);

        // En lista för random  bara 
        //var quizList = new List<object?>
        //{
        //    trivia,
        //    quiz
        //};

        // Slumpa vilken som ska exponeras utåt
        //Random random = new Random();
        //var randomQuiz = quizList.ElementAt(random.Next(0, quizList.Count));

        //return randomQuiz as QuizModel;
    }

    public async Task<bool> CheckTriviaQuestionIdInDb(Guid id)
    {
        return await _questionService.QuestionExists(id);
    }
    
    // Konverterar triviaModel till quizModel
    public async Task<QuizModel?> GetSingleTrivia()
    {
        var response = await _triviaRepository.GetTriviaQuiz();
        var answersList = new List<string>();

        QuizModel? responseQuiz = null;

        if (response is null)
        {
            return null;
        }

        foreach (var quiz in response)
        {
            Guid triviaId = Guid.Parse(quiz.Id?.PadLeft(32, '0'));
            // quiz.IncorrectAnswers.ForEach(a => answersList.Add(a));
            //Random rand = new Random();
            //answersList.Add(quiz.CorrectAnswer);
            //var shuffledList = answersList.OrderBy(_ => rand.Next()).ToList();
            // quiz.IncorrectAnswers = shuffledList;
            //responseQuiz = new QuizModel(quiz.Category, triviaId, quiz.Question, quiz.IncorrectAnswers);
            var question = await _questionService.GetQuestion(triviaId);

            // DÅ FINNS DEN I DATABASEN
            if (question != null)
            {
                // var quizModel = new QuizModel(question.Category, question.Id, question.QuestionText );

                QuizModel quizModel = new QuizModel
                {
                    Id = question.Id,
                    Question = question.QuestionText,
                    Category = question.Category,
                    Answers = new List<string>(),
                };
                
              //  await _questionService.AddQuestion();
              
                var filteredAnswers = await _answerService.Get(question.Id);
                //Random random = new Random();
                shuffledAnswersList = filteredAnswers.ToList();
                Debug.WriteLine("!!!!!!!!!!!!!");
                for (int i = 0; i < shuffledAnswersList.Count(); i++)
                {

                    Debug.WriteLine(shuffledAnswersList[i].AnswerText + " " + shuffledAnswersList[i].IsCorrectAnswer);
                }
                Debug.WriteLine("!!!!!!!!!!!!!");
                foreach (var answer in filteredAnswers)
                {
                    if (answer != null)
                    {
                        Console.WriteLine($"{answer.AnswerText}");
                        Console.WriteLine($"{answer.QuestionId}");
                        quizModel.Answers.Add("2. "+answer.AnswerText);
                    }

                }
                
                return quizModel;
                //var responseQuestion = new QuestionCreateRequest(responseQuiz.Question, responseQuiz.Category, responseQuiz.Id);
                // await _questionService.AddQuestion(responseQuestion);
                // Hämtar en lista med alla matchande Id:n
            }

            var triviaQuestion = new QuestionCreateRequest();
            triviaQuestion.QuestionText = quiz.Question;
            triviaQuestion.Category = quiz.Category;
            triviaQuestion.Id = triviaId;
            
            //Console.WriteLine($"1111111111111111111111 {triviaQuestion.QuestionText}{triviaId}");
            await _questionService.AddQuestion(triviaQuestion);

            var correctAnswer = new AnswerCreateRequest();
            correctAnswer.AnswerText = quiz.CorrectAnswer;
            correctAnswer.IsCorrectAnswer = true;
            correctAnswer.QuestionId = triviaId;
            //Console.WriteLine($"1111111111111111111111 {correctAnswer.AnswerText}{correctAnswer.QuestionId}");
            await _answerService.AddAnswer(correctAnswer);
            //Console.WriteLine($"1111111111111111111111 {correctAnswer.AnswerText}{correctAnswer.QuestionId}");

            foreach (var a in quiz.IncorrectAnswers)
            {
                var answer = new AnswerCreateRequest();
                answer.AnswerText = a;
                answer.IsCorrectAnswer = false;
                answer.QuestionId = triviaId;
                //Console.WriteLine($"1111111111111111111111 {answer.AnswerText}{answer.QuestionId}");
                await _answerService.AddAnswer(answer);
                //Console.WriteLine($"1111111111111111111111 {answer.AnswerText}{answer.QuestionId}");
            }
            /*
           responseQuiz = new QuizModel
            {
                Id = triviaId,
                Question = triviaQuestion.QuestionText,
                Category = triviaQuestion.Category,
                Answers = new List<string>(),
            };

            var filtered = await _answerService.Get(triviaId);

            foreach (var answer in filtered)
            {
                if (answer != null)
                {
                    Console.WriteLine($"{answer.AnswerText}");
                    Console.WriteLine($"{answer.QuestionId}");
                    responseQuiz.Answers.Add(answer.AnswerText);
                }

            }

            return responseQuiz;
            */
            return null;
        }
      
        return responseQuiz;
    }
    
    
    // Mapppar Trivia till Answers

    public async void SaveTriviaQuestion(TriviaModel question)
    {
        var triviaQuestion = new QuestionCreateRequest();
        triviaQuestion.QuestionText = question.Question;
        triviaQuestion.Category = question.Category;
        await _questionService.AddQuestion(triviaQuestion);
    }

    public async void SaveCorrectTriviaAnswers(TriviaModel triviaQuestion, Guid questionId)
    {
        var answer = new AnswerCreateRequest();
        answer.AnswerText = triviaQuestion.CorrectAnswer;
        answer.IsCorrectAnswer = true;
        answer.QuestionId = questionId;
        await _answerService.AddAnswer(answer);
    }

    public async void SaveIncorrectTriviaAnswers(String incorrectAnswer, Guid questionId)
    {
        var answer = new AnswerCreateRequest();
        answer.AnswerText = incorrectAnswer;
        answer.IsCorrectAnswer = false;
        answer.QuestionId = questionId;
        await _answerService.AddAnswer(answer);
    }
    
    // Konverterar question till quizmodel, Ska bli quiz 
    public async Task<QuizModel> GetQuizFromDb()
    {
        // Hämta en fråga
        var responseQuestion = await _questionService.GetRandomQuestion();
        
        QuizModel responseQuiz = new QuizModel
        {
            Id = responseQuestion.Id,
            Question = responseQuestion.QuestionText,
            Category = responseQuestion.Category,
            Answers = new List<string>(),
        };

        Console.WriteLine($"{responseQuestion.Id}");
        // Hämtar alla svar med samma questionId som question från Answers
        var filteredAnswers = await _answerService.Get(responseQuestion.Id);
        Random random = new Random();
         shuffledAnswersList = filteredAnswers.OrderBy(_ => random.Next()).ToList();
        Debug.WriteLine("!!!!!!!!!!!!!");
        for (int b = 0; b < shuffledAnswersList.Count(); b++)
        {

            Debug.WriteLine(shuffledAnswersList[b].AnswerText + " " + shuffledAnswersList[b].IsCorrectAnswer);
        }
        Debug.WriteLine("!!!!!!!!!!!!!");
        var i = 1;
        foreach (var answer in shuffledAnswersList)
        {
            if (answer != null)
            {
                Console.WriteLine($"{answer.AnswerText}");
                Console.WriteLine($"{answer.QuestionId}");
                responseQuiz.Answers.Add($"{i}. "+answer.AnswerText);
            }
            i++;
        }
      
        return responseQuiz;
    }

    /*public async Task<QuizModel?> GetAnswersFromDb()
    {
        var response = await _answerService.AllAnswers();
        QuizModel? responseQuiz = null;

        // genom foreach får vi en och inte en lista, lägg dom i en lista?
        foreach (var quiz in response)
        {
            var answersList = new List<string>();
            answersList.Add(quiz.AnswerText);
            
            responseQuiz = new QuizModel()
            {
               // Answers = quiz.AnswerText;
            };
        }

        return responseQuiz;
    }*/



    public async Task<string> getTrue(int input)
    {
        Debug.WriteLine("!!!!!!!!!!!!!");
        for (int i = 0; i < shuffledAnswersList.Count(); i++)
        {

            Debug.WriteLine(shuffledAnswersList[i].AnswerText + " " + shuffledAnswersList[i].IsCorrectAnswer);
        }
        Debug.WriteLine("!!!!!!!!!!!!!");
       
        if (input <= 0) { return "PLs dont type 0! One of the answers numbers!"; }
        if (input > shuffledAnswersList.Count()) { return "Pls choose one of the answers numbers!"; }

        if (shuffledAnswersList[input-1].IsCorrectAnswer.Equals(true)) { return $"You choose {shuffledAnswersList[input-1].AnswerText}, You got it right!"; }
        
        return $"You choose {shuffledAnswersList[input-1].AnswerText},Sry wrong answer!";

    }
}