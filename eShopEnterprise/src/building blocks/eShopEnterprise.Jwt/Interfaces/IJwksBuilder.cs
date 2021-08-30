using Microsoft.Extensions.DependencyInjection;

namespace eShopEnterprise.Jwt.Interfaces
{
    public interface IJwksBuilder
    {
        IServiceCollection Services { get; }
    }
}
