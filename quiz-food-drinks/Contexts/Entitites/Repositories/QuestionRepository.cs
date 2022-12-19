using Microsoft.EntityFrameworkCore;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.ViewModels.Question.cs;

namespace quiz_food_drinks.Contexts.Entitites.Repositories;

internal class QuestionRepository : IQuestionRepository
{
    private readonly QuizDatabaseContext _context;

    public QuestionRepository(QuizDatabaseContext context)
    {
        _context = context;
    }

    public List<Question> GetQuestions()
    {
        return _context.Questions.ToList();
    }

    public Question? Get(Guid Id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Question> AddAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public Question? Put(Question question)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Question question)
    {
        throw new NotImplementedException();
    }
}