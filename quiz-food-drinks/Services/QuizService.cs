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
                    var counterN = 1;
                    if (answer != null)
                    {
                        Console.WriteLine($"{answer.AnswerText}");
                        Console.WriteLine($"{answer.QuestionId}");
                        quizModel.Answers.Add($"{counterN}. "+answer.AnswerText);
                    }
                    counterN++;
                }

                return quizModel;
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

        return responseQuiz;
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
        var counterN = 1;
        foreach (var answer in shuffledAnswersList)
        {
            if (answer != null)
            {
                Console.WriteLine($"{answer.AnswerText}");
                Console.WriteLine($"{answer.QuestionId}");
                responseQuiz.Answers.Add($"{counterN}. "+answer.AnswerText);
            }
            counterN++;
        }

        return responseQuiz;
    }

    /*public async Task<QuizModel?> GetAnswersFromDb()
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

        return responseQuiz;
    }*/



    public  ActionResult<string>? getTrue(int input)
    {
        /* Debug.WriteLine("!!!!!!!!!!!!!");
         for (int i = 0; i < shuffledAnswersList.Count(); i++)
         {

             Debug.WriteLine(shuffledAnswersList[i].AnswerText + " " + shuffledAnswersList[i].IsCorrectAnswer);
         }
         Debug.WriteLine("!!!!!!!!!!!!!");
        */
        if (shuffledAnswersList==null) { return null; }
        if (input <= 0) { return "PLs dont type 0! One of the answers numbers!"; }
        if (input > shuffledAnswersList.Count()) { return "Pls choose one of the answers listed by the question!"; }

        if (shuffledAnswersList[input-1].IsCorrectAnswer.Equals(true)) { return $"You choose {shuffledAnswersList[input-1].AnswerText}, You got it right!"; }

        return $"You choose {shuffledAnswersList[input-1].AnswerText},Sry wrong answer!";

    }
}
