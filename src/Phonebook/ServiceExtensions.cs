using Microsoft.Extensions.DependencyInjection;
using Phonebook.Services.Person;
using System;
using System.IO;
using System.Reflection;

namespace Phonebook
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Phonebook",
                    Version = "v1",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Vitor Xavier de Souza",
                        Email = "vitorvxs@live.com",
                        Url = "https://github.com/Vitor-Xavier"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureScopes(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
