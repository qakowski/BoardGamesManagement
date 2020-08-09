using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BoardGamesManagement.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "API for board games",
                    Title = "BoardGamesManagement",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = "lukasz.glowinski@o2.pl",
                        Name = "Lukasz Glowinski",
                        Url = new Uri("https://lglowinski.pl")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
