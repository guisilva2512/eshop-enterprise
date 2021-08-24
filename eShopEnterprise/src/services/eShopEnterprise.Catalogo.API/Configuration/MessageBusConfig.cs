using eShopEnterprise.Catalogo.API.Services;
using eShopEnterprise.Core.Utils;
using eShopEnterprise.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopEnterprise.Catalogo.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CatalogoIntegrationHandler>();
        }
    }
}
