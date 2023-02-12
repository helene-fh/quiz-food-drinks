using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;
using quiz_food_drinks.ViewModels;
using quiz_food_drinks.ViewModels.Answer.cs;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Services;

public class QuizService : IQuizService
{
    private readonly IQuestionService _questionService;
    private readonly IAnswerService _answerService;
    private readonly ITriviaRepository _triviaRepository;
    private static List<Answer>? shuffledAnswersList;
    private QuizModel? trivia;
    private QuizModel quiz;

    public QuizService(IQuestionService questionService, IAnswerService answerService, ITriviaRepository triviaRepository)
    {
        _questionService = questionService;
        _answerService = answerService;
        _triviaRepository = triviaRepository;
    }

    public async Task<QuizModel?> GetRandomQuiz()
    {
        Random random = new Random();
        int source = random.Next(2);

        if (source == 0)
        {
           return await GetSingleTrivia();
        }

        var dbQuiz = await GetQuizFromDb();
        if (dbQuiz != null)
        {
            return dbQuiz;
        }

        return await GetSingleTrivia();
    }


    public async Task<QuizModel?> GetSingleTrivia()
    {
        QuizModel? quizModel = null;
        var response = await _triviaRepository.GetTriviaQuiz();

        if (response is null)
        {
            return null;
        }

        foreach (var quiz in response)
        {
            Guid triviaId = Guid.Parse(quiz.Id?.PadLeft(32, '0'));

            var question = await _questionService.GetQuestion(triviaId);

            if (question != null)
            {
                quizModel = new QuizModel
                {
                    Id = question.Id,
                    Question = question.QuestionString,
                    Category = question.Category,
                    Answers = new List<string>(),
                };

                await AddRandomizedAnswersList(quizModel, question);
                return quizModel;
            }

            SaveTriviaQuestion(response[0], triviaId);
            SaveCorrectTriviaAnswers(quiz, triviaId);
            SaveIncorrectTriviaAnswers(quiz.IncorrectAnswers, triviaId);
            var newQuestion = await _questionService.GetQuestion(triviaId);
            if (newQuestion != null)
            {
                quizModel = new QuizModel
                {
                    Id = newQuestion.Id,
                    Question = newQuestion.QuestionString,
                    Category = newQuestion.Category,
                    Answers = new List<string>(),
                };

                await AddRandomizedAnswersList(quizModel, newQuestion);
                return quizModel;
            }
            return null;
        }

        return quizModel;

    }


    private async Task<Question?> CheckTriviaQuestionInDb(Question? question)
    {
        if (question != null)
        {
            QuizModel quizModel = new QuizModel
            {
                Id = question.Id,
                Question = question.QuestionString,
                Category = question.Category,
                Answers = new List<string>(),
            };

            await AddRandomizedAnswersList(quizModel, question);
        }

        return question;
    }
    private async void SaveTriviaQuestion(TriviaModel question, Guid triviaId)
    {
        var triviaQuestion = new QuestionCreateRequest();
        triviaQuestion.QuestionString = question.Question;
        triviaQuestion.Category = question.Category;
        triviaQuestion.Id = triviaId;
        await _questionService.AddQuestion(triviaQuestion);
    }
    private async void SaveCorrectTriviaAnswers(TriviaModel triviaQuestion, Guid triviaId)
    {
        var answer = new AnswerCreateRequest();
        answer.AnswerText = triviaQuestion.CorrectAnswer;
        answer.IsCorrectAnswer = true;
        answer.QuestionId = triviaId;
        await _answerService.AddAnswer(answer);
    }

    private async void SaveIncorrectTriviaAnswers(List<string> incorrectAnswers, Guid triviaId)
    {
        foreach (var answerText in incorrectAnswers)
        {
            var answer = new AnswerCreateRequest();
            answer.AnswerText = answerText;
            answer.IsCorrectAnswer = false;
            answer.QuestionId = triviaId;
            await _answerService.AddAnswer(answer);
        }
    }

    private async Task<QuizModel?> GetQuizFromDb()
    {
        var responseQuestion = await _questionService.GetRandomQuestion();

        if (responseQuestion is null)
        {
            return null;
        }

        QuizModel responseQuiz = new QuizModel
        {
            Id = responseQuestion.Id,
            Question = responseQuestion.QuestionString,
            Category = responseQuestion.Category,
            Answers = new List<string>(),
        };

        await AddRandomizedAnswersList(responseQuiz, responseQuestion);

        return responseQuiz;
    }

    public async Task<bool> CheckTriviaQuestionIdInDb(Guid id)
    {
        return await _questionService.QuestionExists(id);
    }

    private async Task<List<Answer?>> AddRandomizedAnswersList(QuizModel responseQuiz, Question responseQuestion)
    {
        Random random = new Random();
        var filteredList = await _answerService.Get(responseQuestion.Id);
        var shuffledAnswersList = filteredList.OrderBy(_ => random.Next()).ToList();
        
         foreach (var answer in shuffledAnswersList)
         {
             if (answer != null)
             {
                 responseQuiz.Answers.Add(answer.AnswerText);
             }
         }

         return shuffledAnswersList;
    }



    public  ActionResult<string>? getTrue(int input)
    {

        if (shuffledAnswersList==null) { return null; }
        if (input <= 0) { return "PLs dont type 0! One of the answers numbers!"; }
        if (input > shuffledAnswersList.Count()) { return "Pls choose one of the answers listed by the question!"; }

        if (shuffledAnswersList[input-1].IsCorrectAnswer.Equals(true)) { return $"You choose {shuffledAnswersList[input-1].AnswerText}, You got it right!"; }

        return $"You choose {shuffledAnswersList[input-1].AnswerText},Sry wrong answer!";

    }
    
}
