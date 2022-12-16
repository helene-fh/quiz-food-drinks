using Microsoft.EntityFrameworkCore;
using quiz_food_drinks.Configurations.Dependencies;
using quiz_food_drinks.Configurations.EntityFramework;
using quiz_food_drinks.Persistance;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureDependencies(config);
//builder.Services.AddScoped<QuizDatabaseContext>();
builder.Services.AddQuizDbContextUsingSQLLite();
/*builder.Services.AddDbContext<QuizDatabaseContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("QuizDataBaseContext")));*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();