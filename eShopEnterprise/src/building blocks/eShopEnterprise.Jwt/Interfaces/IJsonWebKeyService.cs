using Microsoft.IdentityModel.Tokens;

namespace eShopEnterprise.Jwt.Interfaces
{
    public interface IJsonWebKeyService
    {
        JsonWebKey Generate(Algorithm jwsAlgorithm);
    }
}
