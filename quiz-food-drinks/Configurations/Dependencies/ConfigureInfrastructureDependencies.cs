using quiz_food_drinks.Contexts.Entitites.Repositories;
using quiz_food_drinks.Contexts.Trivia.Repositories;
using quiz_food_drinks.Interfaces.Repositories;


namespace quiz_food_drinks.Configurations.Dependencies;

public static class ConfigureInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,
        IConfiguration config)
    {

        return services
            .AddScoped<ITriviaRepository, TriviaRepository>()
            .AddScoped<IQuestionRepository, QuestionRepository>();
    }
}
                                        