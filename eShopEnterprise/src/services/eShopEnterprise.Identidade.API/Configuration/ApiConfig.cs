using eShopEnterprise.Identidade.API.Services;
using eShopEnterprise.Jwt.AspNetCore;
using eShopEnterprise.WebApi.Core.Identidade;
using eShopEnterprise.WebApi.Core.Usuario;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using NetDevPack.Security.Jwt.AspNetCore;

namespace eShopEnterprise.Identidade.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<AuthenticationService>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseJwksDiscovery(); //building blocks\eShopEnterprise.Jwt.AspNetCore\AspNetBuilderExtensions.cs

            return app;
        }
    }
}
