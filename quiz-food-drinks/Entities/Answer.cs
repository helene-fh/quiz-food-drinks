using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace quiz_food_drinks.Entities;

public class Answer : BaseEntity, IAnswer
{
    [SwaggerSchema(Description = "Question id (Guid)")]

    public Guid QuestionId { get; set; }

    [SwaggerSchema(Description = "Some text")]

    [DefaultValue("Yoda")]
    public string AnswerText { get; set; } = null!;

    [SwaggerSchema(Description = "true or false")]

    [DefaultValue(false)]
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