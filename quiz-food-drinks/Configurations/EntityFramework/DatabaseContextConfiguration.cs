using Microsoft.EntityFrameworkCore;
using quiz_food_drinks.Persistance;

namespace quiz_food_drinks.Configurations.EntityFramework;

public static class DatabaseContextConfiguration
{
    public static IServiceCollection AddQuizDbContextUsingSQLLite(this IServiceCollection services, string fileName = "QuizSqlLight.db")
    {
        return services
            .AddDbContext<QuizDatabaseContext>(options =>
                    options.UseSqlite($"Data Source={fileName}"),
                ServiceLifetime.Scoped
            );
    }
}