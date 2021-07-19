using eShopEnterprise.Clientes.API.Application.Commands;
using eShopEnterprise.Clientes.API.Application.Events;
using eShopEnterprise.Clientes.API.Data.Repository;
using eShopEnterprise.Clientes.API.Models;
using eShopEnterprise.Core.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eShopEnterprise.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            //services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            //services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
        }

        public static void UseApiConfiguration(IApplicationBuilder app)
        {

        }
    }
}
