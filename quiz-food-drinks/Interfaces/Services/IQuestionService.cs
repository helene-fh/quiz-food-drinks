using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Interfaces.Services;

public interface IQuestionService
{
    //QuestionResponse GetQuestion(Guid id);
    //List<QuestionResponse> GetQuestions();
    //void AddQuestion(QuestionCreateRequest question);
    Task<Question> AddQuestion(QuestionCreateRequest question);
}