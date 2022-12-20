using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels;

namespace quiz_food_drinks.Models;

public class QuizModel 
{
    
    // Något Id måste va identiskt med answer.QuestionId, question.Id, trivia.Id
    public Guid Id { get; set; }

    public string TriviaId { get; set; }

    public string Question { get; set; }
    
    public List<string> Answers { get; set; }
    
    public string Category { get; set; }

    public QuizModel()
    {
    }

    public QuizModel(string triviaId, string question, List<string> answers, string category)
    {
        Id = Guid.NewGuid();
        TriviaId = triviaId;
        Question = question;
        Answers = answers;
        Category = category;
    }

    public QuizModel(string quizCategory, string quizId, string quizQuestion)
    {
        Id = Guid.NewGuid();
        Category = quizCategory;
        TriviaId = quizId;
        Question = quizQuestion;

    }
}