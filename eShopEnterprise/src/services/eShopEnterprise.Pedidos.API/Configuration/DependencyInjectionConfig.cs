using eShopEnterprise.Core.Mediator;
using eShopEnterprise.Pedidos.API.Application.Commands;
using eShopEnterprise.Pedidos.API.Application.Events;
using eShopEnterprise.Pedidos.API.Application.Queries;
using eShopEnterprise.Pedidos.Domain.Pedidos;
using eShopEnterprise.Pedidos.Domain.Vouchers;
using eShopEnterprise.Pedidos.Infra.Data;
using eShopEnterprise.Pedidos.Infra.Data.Repository;
using eShopEnterprise.WebApi.Core.Usuario;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace eShopEnterprise.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            // Commands
            services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, PedidoCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            // Data
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<PedidosContext>();
        }
    }
}