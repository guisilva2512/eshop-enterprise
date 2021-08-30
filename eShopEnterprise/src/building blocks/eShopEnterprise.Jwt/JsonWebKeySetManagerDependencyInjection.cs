using eShopEnterprise.Jwt.DefaultStore;
using eShopEnterprise.Jwt.Interfaces;
using eShopEnterprise.Jwt.Jwk;
using eShopEnterprise.Jwt.Jwks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace eShopEnterprise.Jwt
{
    public static class JsonWebKeySetManagerDependencyInjection
    {
        /// <summary>
        /// Sets the signing credential.
        /// </summary>
        /// <returns></returns>
        public static IJwksBuilder AddJwksManager(this IServiceCollection services, Action<JwksOptions> action = null)
        {
            if (action != null)
                services.Configure(action);

            services.AddDataProtection();
            services.AddScoped<IJsonWebKeyService, JwkService>();
            services.AddScoped<IJsonWebKeySetService, JwksService>();
            services.AddSingleton<IJsonWebKeyStore, DataProtectionStore>();

            return new JwksBuilder(services);
        }
    }

    public class JwksBuilder : IJwksBuilder
    {

        public JwksBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services { get; }
    }
}
