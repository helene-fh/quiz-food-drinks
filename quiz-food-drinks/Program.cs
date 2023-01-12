using System.Reflection;
using System.Text.Json.Serialization;
using quiz_food_drinks.Configurations.Dependencies;
using quiz_food_drinks.Configurations.EntityFramework;
using quiz_food_drinks.Configurations.Swagger;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Services;



internal class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        ConfigurationManager config = builder.Configuration;

        builder.Services.AddQuizDbContextUsingSqlLite();
        builder.Services.AddControllers()
            .AddJsonOptions(SwaggerJsonOptionsUsingEnumAsString);

        builder.Services.AddInfrastructureDependencies(config);

        builder.Services.AddSwaggerConfigurations();

        builder.Services.AddResponseCompression();

        RunWebApplication(builder);
    }

    static readonly Action<Microsoft.AspNetCore.Mvc.JsonOptions> SwaggerJsonOptionsUsingEnumAsString = (options) => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    };

    /// <summary>
    /// Build and start the web application
    /// </summary>
    /// <param name="builder"></param>
    static void RunWebApplication(WebApplicationBuilder builder)
    {
        WebApplication app = builder.Build();

        app.UseResponseCompression();

        app.UseSwaggerConfigurations();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
