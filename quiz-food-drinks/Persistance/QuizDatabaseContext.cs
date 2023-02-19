using Microsoft.EntityFrameworkCore;
using quiz_food_drinks.Entities;

namespace quiz_food_drinks.Persistance;

public class QuizDatabaseContext : DbContext
{
    public DbSet<Question> Questions { get; set; } 
    public DbSet<Answer> Answers { get; set; } 

    public QuizDatabaseContext() 
    { 

    }

    public QuizDatabaseContext(DbContextOptions<QuizDatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite("Data Source=QuizSqlLight.db");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CreateQuizModel(modelBuilder);
    }

    private static void CreateQuizModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasOne<Question>().WithMany().HasForeignKey(q => q.QuestionId);
        });
    }
}