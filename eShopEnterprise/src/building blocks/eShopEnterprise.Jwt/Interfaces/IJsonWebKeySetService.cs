using eShopEnterprise.Jwt.Model;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace eShopEnterprise.Jwt.Interfaces
{
    public interface IJsonWebKeySetService
    {
        SigningCredentials GenerateSigningCredentials(JwksOptions options = null);
        //SigningCredentials GetCurrentSigningCredentials(JwksOptions options = null);
        //EncryptingCredentials GetCurrentEncryptingCredentials(JwksOptions options = null);
        //EncryptingCredentials GenerateEncryptingCredentials(JwksOptions options = null);
        IReadOnlyCollection<JsonWebKey> GetLastKeysCredentials(JsonWebKeyType jsonWebKeyType, int qty);

    }
}
