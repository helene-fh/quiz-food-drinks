using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Interfaces.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>> GetQuestionsAsync();
    Task<Question?> GetAsync(Guid id);
    Task<Question> AddAsync(Question question);
    Task<Question?> UpdateAsync(QuestionUpdateRequest question);
    Task<Question?> DeleteAsync(Guid question);
    Task<bool> QuestionExists(Guid id);
}