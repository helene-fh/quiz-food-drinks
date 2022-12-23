using System.Reflection;
using quiz_food_drinks.Configurations.Dependencies;
using quiz_food_drinks.Configurations.EntityFramework;
using quiz_food_drinks.Configurations.SwaggerSchema;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddInfrastructureDependencies(config);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(
    a =>
    {
        a.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {

            Title = "Quiz-food-drinks",
            Version = "v1",
            Description = "Made by Helene and Vincent"
        });
        //Using System.Reflection;
        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        a.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, xmlFileName));
        a.SchemaFilter<SwaggerSchemaExampleFIlter>();
        a.EnableAnnotations();
        a.SwaggerDoc("v1-1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "DemoSwaggerAnnotation",
            Version = "v1-1"

        });
        

    });


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });



});

builder.Services.AddQuizDbContextUsingSqlLite();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;


});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




