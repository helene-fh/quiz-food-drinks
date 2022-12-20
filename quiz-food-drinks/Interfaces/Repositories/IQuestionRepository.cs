using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Interfaces.Repositories;

public interface IQuestionRepository
{
    public Task<List<Question>> GetQuestionsAsync();
    public Task<Question?> GetAsync(Guid id);

    public Task<Question> AddAsync(Question question);
    public Task<Question?> UpdateAsync(QuestionUpdateRequest question);
    public Task<Question?> DeleteAsync(Guid question);

    public Task<bool> QuestionExists(Guid id);

}