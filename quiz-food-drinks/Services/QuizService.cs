using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Models;

namespace quiz_food_drinks.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuestionService _questionService;
    private readonly IAnswerService _answerService;
    private readonly ITriviaRepository _triviaRepository;

    public QuizService(IQuizRepository quizRepository, IQuestionService questionService, IAnswerService answerService, ITriviaRepository triviaRepository)
    {
        _quizRepository = quizRepository;
        _questionService = questionService;
        _answerService = answerService;
        _triviaRepository = triviaRepository;
    }
    

    public async Task<QuizModel?> GetRandomQuiz()
    {
        var trivia = await GetSingleTrivia();
        var quiz = await GetQuizFromDb();

        //var triviaId = trivia.Id;
       // var questionId = question.Id;

       // LINQ som går in Questions och ser om triviaId finns
        
        //Addera question i databas 
        //var newQuestion = new QuestionCreateRequest();
        //await _questionService.AddQuestion(newQuestion);

        // En lista för random  bara 
        var quizList = new List<object?>
        {
            trivia,
            quiz
        };

        // Slumpa vilken som ska exponeras utåt
        Random random = new Random();
        var randomQuiz = quizList.ElementAt(random.Next(0, quizList.Count));

        return randomQuiz as QuizModel;
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

        if (response != null)
            foreach (var quiz in response)
            {
                quiz.IncorrectAnswers.ForEach(a => answersList.Add(a));
                Random rand = new Random();
                answersList.Add(quiz.CorrectAnswer);
                var shuffledList = answersList.OrderBy(_ => rand.Next()).ToList();

                quiz.IncorrectAnswers = shuffledList;

                responseQuiz = new QuizModel(quiz.Category, quiz.Id, quiz.Question, quiz.IncorrectAnswers);
            }

        return responseQuiz;
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
            Category = responseQuestion.Category
        };

        Console.WriteLine($"{responseQuestion.Id}");
        // Hämtar alla svar med samma questionId som question från Answers
        var filteredAnswers = await _answerService.Get(responseQuestion.Id);
        Random random = new Random();
        var shuffledAnswersList = filteredAnswers.OrderBy(_ => random.Next()).ToList();
        
        foreach (var answer in shuffledAnswersList)
        {
            if (answer != null)
            {
                Console.WriteLine($"{answer.AnswerText}");
                Console.WriteLine($"{answer.QuestionId}");
                //responseQuiz.Answers.Add(answer.AnswerText);
            }

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
    

}