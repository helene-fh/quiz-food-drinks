using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Entities;

public class Answer : BaseEntity, IAnswer
{
    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrectAnswer { get; set; }

    public Answer()
    {

    }


    public Answer(Guid questionId, string answerText, bool isCorrectAnswer)
    {
        QuestionId = questionId;
        AnswerText = answerText;
        IsCorrectAnswer = isCorrectAnswer;
    }



}