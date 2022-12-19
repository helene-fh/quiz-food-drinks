using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Interfaces.Services;

public interface IQuestionService
{
    Task<List<Question>> AllQuestions();
    Task<Question?> GetQuestion(Guid id);
    Task<Question> AddQuestion(QuestionCreateRequest question);
    Task<Question?> DeleteQuestion(Guid id);
    Task<Question?> UpdateQuestion(QuestionUpdateRequest question);
}