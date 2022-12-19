using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Services;

public class QuestionService : IQuestionService
{

    private readonly IQuestionRepository _questionRepository;
    
    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public List<Question> Get()
    {
        return _questionRepository.GetQuestions();
    }
    
    public Task<Question> AddQuestion(QuestionCreateRequest question)
    {
      /*  var newQuestion = new Question()
        {
            Id = Guid.NewGuid(),
            QuestionText = question.QuestionText,
            Category = question.Category
        };
        
        return _questionRepository.AddAsync(newQuestion);*/
      
       var newQuestion = new Question(question.QuestionText, question.Category);
       return _questionRepository.AddAsync(newQuestion);
    }
}