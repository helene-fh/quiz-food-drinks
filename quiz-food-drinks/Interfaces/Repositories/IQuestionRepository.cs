using quiz_food_drinks.Entities;

namespace quiz_food_drinks.Interfaces.Repositories;

public interface IQuestionRepository
{
    public List<Question> GetQuestions();
    public Question? Get(Guid Id);

    Task<Question> AddAsync(Question question);
    public Question? Put(Question question);
    public bool Delete(Question question);
    
}