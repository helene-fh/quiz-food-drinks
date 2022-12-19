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
    
    public async Task<List<Question>> AllQuestions()
    {
        return await _questionRepository.GetQuestionsAsync();
    }
    
    public async Task<Question?> GetQuestion(Guid id)
    {
        return await _questionRepository.GetAsync(id);
    }

    public async Task<Question> AddQuestion(QuestionCreateRequest question)
    {
      var newQuestion = new Question()
        {
            Id = Guid.NewGuid(),
            QuestionText = question.QuestionText,
            Category = question.Category
        };
      
       //var newQuestion = new Question(question.QuestionText, question.Category);
       return await _questionRepository.AddAsync(newQuestion);
    }
    
    public async Task<Question?> UpdateQuestion(QuestionUpdateRequest question)
    {
        return await _questionRepository.UpdateAsync(question);
    }
    public async Task<Question?> DeleteQuestion(Guid id)
    {
        return await _questionRepository.DeleteAsync(id);
        
    }
    
}