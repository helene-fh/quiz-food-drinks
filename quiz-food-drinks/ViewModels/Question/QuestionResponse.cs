using quiz_food_drinks.Interfaces.Entitites;

namespace quiz_food_drinks.ViewModels.Question.cs;

public class QuestionResponse : QuestionBase
{
    public QuestionResponse(IQuestion question) : base()
    {
        base.QuestionText = question.QuestionText;
        base.Category = question.Category;
    }
}