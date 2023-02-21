using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;
using quiz_food_drinks.ViewModels.Answer.cs;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Services;

public class QuizService : IQuizService
{
    private readonly IQuestionService _questionService;
    private readonly IAnswerService _answerService;
    private readonly ITriviaRepository _triviaRepository;
    private static List<Answer?>? shuffledAnswersList;

    public QuizService(IQuestionService questionService, IAnswerService answerService, ITriviaRepository triviaRepository)
    {
        _questionService = questionService;
        _answerService = answerService;
        _triviaRepository = triviaRepository;
    }

    public async Task<QuizModel?> GetRandomQuiz()
    {
        Random random = new();
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
            Guid triviaId = Guid.Parse(quiz.Id.PadLeft(32, '0'));

            var question = await _questionService.GetQuestion(triviaId);

             if (question != null)
             {
               return await CheckTriviaQuestionInDb(question, quizModel);
            }
           
            SaveTriviaQuestion(response[0], triviaId);
            SaveCorrectTriviaAnswers(quiz, triviaId);
            SaveIncorrectTriviaAnswers(quiz.IncorrectAnswers, triviaId);
            var newQuestion = await _questionService.GetQuestion(triviaId);

            if (newQuestion != null)
            {
                QuizModel? newQuiz = await CheckTriviaQuestionInDb(newQuestion, null);
                return newQuiz;
            }
            return null;
        }

        return quizModel;

    }

    private async Task<QuizModel?> CheckTriviaQuestionInDb(Question question, QuizModel? quizModel)
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

    private async void SaveTriviaQuestion(TriviaModel question, Guid triviaId)
    {
        var triviaQuestion = new QuestionCreateRequest
        {
            QuestionString = question.Question,
            Category = question.Category,
            Id = triviaId
        };
        await _questionService.AddQuestion(triviaQuestion);
    }

    private async void SaveCorrectTriviaAnswers(TriviaModel triviaQuestion, Guid triviaId)
    {
        var answer = new AnswerCreateRequest
        {
            AnswerText = triviaQuestion.CorrectAnswer,
            IsCorrectAnswer = true,
            QuestionId = triviaId
        };
        await _answerService.AddAnswer(answer);
    }

    private async void SaveIncorrectTriviaAnswers(List<string> incorrectAnswers, Guid triviaId)
    {
        foreach (var answerText in incorrectAnswers)
        {
            var answer = new AnswerCreateRequest
            {
                AnswerText = answerText,
                IsCorrectAnswer = false,
                QuestionId = triviaId
            };
            await _answerService.AddAnswer(answer);
        }
    }

    private async Task<QuizModel?> GetQuizFromDb()
    {
        var responseQuestion = await _questionService.GetRandomQuestion();
        QuizModel? quizModel = null;
        
        if (responseQuestion is null)
        {
            return null;
        }

        return await CheckTriviaQuestionInDb(responseQuestion, quizModel);
    }

    public async Task<bool> CheckTriviaQuestionIdInDb(Guid id)
    {
        return await _questionService.QuestionExists(id);
    }

    private async Task<List<Answer?>> AddRandomizedAnswersList(QuizModel responseQuiz, Question responseQuestion)
    {
        Random random = new();
        var filteredList = await _answerService.Get(responseQuestion.Id);
        shuffledAnswersList = filteredList.OrderBy(_ => random.Next()).ToList()!;

        var counterN = 1;
         foreach (var answer in shuffledAnswersList)
         {    
             if (answer != null)
             {
                responseQuiz.Answers.Add($"{counterN}." +answer.AnswerText);
             }

            counterN++;
         }

         return shuffledAnswersList;
    }

    public async Task<AnswerBase?> GetTrue(int answerInputId)
    {
        if (shuffledAnswersList == null) { return null; }
        if (answerInputId <= 0) { var answerEdit = new AnswerCreateRequest("Please dont type 0 or less! Take one of the answers numbers above!");
            return await Task.FromResult(answerEdit); }
        if (answerInputId > shuffledAnswersList.Count) {
            var answerEdit = new AnswerCreateRequest("Please choose one of the answers listed by the question above!");
            return await Task.FromResult(answerEdit); }
        var answerResponse = new AnswerResponse(shuffledAnswersList[answerInputId - 1]!);
        
        return answerResponse;      
    }   
}
