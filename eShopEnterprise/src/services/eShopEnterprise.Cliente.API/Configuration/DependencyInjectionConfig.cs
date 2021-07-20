using eShopEnterprise.Clientes.API.Data.Repository;
using eShopEnterprise.Clientes.API.Models;
using eShopEnterprise.Clientes.API.Services;
using eShopEnterprise.Core.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eShopEnterprise.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddHostedService<RegistroClienteIntegrationHandler>();
        }

        public static void UseApiConfiguration(IApplicationBuilder app)
        {

        }
    }
}
