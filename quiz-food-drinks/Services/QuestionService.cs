using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
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

    public async Task<Question?> GetRandomQuestion()
    {
        Random random = new Random();
        
        var allQuestions = await _questionRepository.GetQuestionsAsync();

        if (allQuestions.Count >= 5)
        {
            var question = allQuestions.ElementAt(random.Next(0, allQuestions.Count));
            return question;
        }

        return null;
    }

    public async Task<Question> AddQuestion(QuestionCreateRequest question)
    {
        var newQuestion = new Question()
        {
            Id = question.Id,
            QuestionString = question.QuestionString,
            Category = question.Category
        };
        
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

    public async Task<bool> QuestionExists(Guid id)
    {
        return await _questionRepository.QuestionExists(id);
    }
    
}