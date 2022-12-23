
namespace quiz_food_drinks.ViewModels.Question.cs;

public class QuestionCreateRequest : QuestionBase
{
    public QuestionCreateRequest(string questionString, string category) : base(questionString, category)
    {
    }

    public QuestionCreateRequest(string responseQuizQuestion, string responseQuizCategory, Guid responseQuizId)
    {

    }

    public QuestionCreateRequest()
    {

    }
}