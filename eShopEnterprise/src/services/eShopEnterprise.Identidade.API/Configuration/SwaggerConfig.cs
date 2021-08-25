using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
namespace eShopEnterprise.Identidade.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "eShop Enterprise Identity API",
                    Description = "Api de autenticação e autorização do grupo eShopEnterprise",
                    Contact = new OpenApiContact()
                    {
                        Name = "Guilherme Silva",
                        Email = "guisilva2512@gmail.com"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "identidade");
            });

            return app;
        }
    }
}
