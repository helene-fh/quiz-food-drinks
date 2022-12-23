using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using quiz_food_drinks.Configurations.SwaggerSchema;
using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace quiz_food_drinks.Entities;

public class Answer : BaseEntity, IAnswer
{
    [SwaggerSchema(Description = "Question id (Guid)")]
    [SwaggerSchemaExample("6B29FC40-CA47-1067-B31D-00DD010662DA")]
    public Guid QuestionId { get; set; }

    [SwaggerSchema(Description = "Some text")]
    [SwaggerSchemaExample("Luke")]
    [DefaultValue("Yoda")]
    public string AnswerText { get; set; } = null!;

    [SwaggerSchema(Description = "true or false")]
    [SwaggerSchemaExample("false")]
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