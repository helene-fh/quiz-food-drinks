using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using quiz_food_drinks.Interfaces.Entitites;
using quiz_food_drinks.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace quiz_food_drinks.Entities;

public class Answer : BaseEntity, IAnswer
{
    [SwaggerSchema(Description = "Question id (Guid)")]
<<<<<<< HEAD
    public Guid QuestionId { get; set; }

    [SwaggerSchema(Description = "Some text")]
=======
    
    public Guid QuestionId { get; set; }

    [SwaggerSchema(Description = "Some text")]
    
>>>>>>> df0772c2225295e59ced701a978b138955f862fe
    [DefaultValue("Yoda")]
    public string AnswerText { get; set; } = null!;

    [SwaggerSchema(Description = "true or false")]
<<<<<<< HEAD
=======
   
>>>>>>> df0772c2225295e59ced701a978b138955f862fe
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