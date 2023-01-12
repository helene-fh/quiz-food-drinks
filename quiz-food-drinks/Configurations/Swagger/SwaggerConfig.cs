using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
//using quiz_food_drinks.Configurations.SwaggerSchema;

namespace quiz_food_drinks.Configurations.Swagger
{
	public static class SwaggerConfig
	{

		public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection service)
		{
            service.AddEndpointsApiExplorer();

            service.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("answer", V1);
                options.SwaggerDoc("question_Trivia", V2);
                options.SwaggerDoc("quiz", V3);


                options.IncludeXmlComments(PathToXMLComments);
               // options.SchemaFilter<SwaggerSchemaExampleFIlter>();
                options.EnableAnnotations();


            });


            service.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

    return service;

            }


        public static WebApplication UseSwaggerConfigurations(this WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options => {
                options.RoutePrefix = "swagger";
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint("quiz/swagger.json", "Quiz V1");
                options.SwaggerEndpoint("question_Trivia/swagger.json", "Question & Trivia V1");
                options.SwaggerEndpoint("answer/swagger.json", "Answer V1");
                
            });

            return app;
        }

        static string PathToXMLComments => Path.Combine(AppContext.BaseDirectory, XmlFileNameFromExecutedAssembly);

        static string XmlFileNameFromExecutedAssembly => $"{Assembly.GetEntryAssembly().GetName().Name}.xml";



        static OpenApiInfo V1 => new OpenApiInfo
        {
            Title = "Answer",
            Version = "v1",
            Description = "Answer CRUD",
            TermsOfService = new Uri("http://toSomewhere.com"),
            Contact = ContactIInformation,
            License = LicenseDescription
        };

        static OpenApiInfo V2 => new OpenApiInfo
        {
            Title = "Question & Trivia",
            Version = "v1",
            Description = "Question CRUD and Question from Trivia",
            TermsOfService = new Uri("http://toSomewhere.com"),
            Contact = ContactIInformation,
            License = LicenseDescription
        };

        static OpenApiInfo V3 => new OpenApiInfo
        {
            Title = "Quiz",
            Version = "v1",
            Description = "Made by Helene & Vincent",
            TermsOfService = new Uri("http://toSomewhere.com"),
            Contact = ContactIInformation,
            License = LicenseDescription
        };

        static OpenApiLicense LicenseDescription => new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
        };

        static OpenApiContact ContactIInformation => new OpenApiContact
        {
            Name = "Please don't contact me",
            Email = "example@queenslab.se"
        };



       



    }
}

