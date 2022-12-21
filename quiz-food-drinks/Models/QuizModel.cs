using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Models;

public class QuizModel 
{
    
    // Något Id måste va identiskt med answer.QuestionId, question.Id, trivia.Id
    public Guid Id { get; set; }

   // public string TriviaId { get; set; } = null!;

    public string Question { get; set; } = null!;

    public List<string> Answers { get; set; } = null!;

    public string Category { get; set; } = null!;

    public QuizModel()
    {
    }
    
    public QuizModel(string quizCategory, Guid quizId, string quizQuestion, List<string> quizIncorrectAnswers)
    {
        Category = quizCategory;
        Id = quizId;
        Question = quizQuestion;
        Answers = quizIncorrectAnswers;
    }

    public QuizModel(string quizCategory, Guid toString, string quizQuestionText)
    {
        Category = quizCategory;
        Id = toString;
        Question = quizQuestionText;
    }

    
    
}