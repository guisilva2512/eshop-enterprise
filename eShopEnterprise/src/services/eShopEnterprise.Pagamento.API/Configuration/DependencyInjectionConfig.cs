﻿using eShopEnterprise.Pagamento.API.Data;
using eShopEnterprise.Pagamento.API.Data.Repository;
using eShopEnterprise.Pagamento.API.Facade;
using eShopEnterprise.Pagamento.API.Models;
using eShopEnterprise.Pagamento.API.Services;
using eShopEnterprise.WebApi.Core.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace eShopEnterprise.Pagamento.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoFacade, PagamentoCartaoCreditoFacade>();

            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<PagamentosContext>();
        }
    }
}
