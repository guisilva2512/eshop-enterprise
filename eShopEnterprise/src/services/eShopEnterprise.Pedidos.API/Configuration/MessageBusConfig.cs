using eShopEnterprise.Core.Utils;
using eShopEnterprise.MessageBus;
using eShopEnterprise.Pedidos.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopEnterprise.Pedidos.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<PedidoOrquestradorIntegrationHandler>()
                .AddHostedService<PedidoIntegrationHandler>();
        }
    }
}
