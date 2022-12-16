using Microsoft.EntityFrameworkCore;
using quiz_food_drinks.Entities;

namespace quiz_food_drinks.Persistance;

public class QuizDatabaseContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Category> Categories { get; set; }

    public QuizDatabaseContext() 
    {

    }

    public QuizDatabaseContext(DbContextOptions<QuizDatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite();

        base.OnConfiguring(optionsBuilder);
    }
    
}