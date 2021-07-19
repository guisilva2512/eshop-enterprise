using eShopEnterprise.Catalogo.API.Data.Repository;
using eShopEnterprise.Catalogo.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopEnterprise.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }

        public static void UseApiConfiguration(IApplicationBuilder app)
        {

        }
    }
}
