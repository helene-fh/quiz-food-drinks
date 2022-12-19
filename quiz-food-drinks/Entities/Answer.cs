using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Entities;

public class Answer : BaseEntity, IAnswer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool IsCorrectAnswer { get; set; }


    public Answer()
    {

    }

    public Answer(string answerText, bool isCorrectAnswer)
    {
        AnswerText = answerText;
        IsCorrectAnswer = isCorrectAnswer;
    }



}