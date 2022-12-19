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

    public async Task<List<Question>> GetQuestions()
    {
        return await _context.Questions.ToListAsync();
    }

    public async Task<Question?> Get(Guid id)
    {
        return _context.Questions
            .Where(x => x.Id == id)
            .FirstOrDefault();
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

    public async Task<Question?> DeleteAsync(Guid id)
    {
        var question = _context.Questions
                .Where(q => q.Id == id)
                .FirstOrDefault();

            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
                
            }
            return question;
    }
    
    public async Task<Question?> UpdateAsync(QuestionUpdateRequest question)
    {
        var updateQuestion = _context.Questions.Where(q => q.Id == question.QuestionId)
                .FirstOrDefault();

        if (updateQuestion != null)
        {
            updateQuestion.QuestionText = question.QuestionText;
            updateQuestion.Category = question.Category;

            _context.Update(updateQuestion);
            await _context.SaveChangesAsync();
        }
        return updateQuestion;
    }
    
}