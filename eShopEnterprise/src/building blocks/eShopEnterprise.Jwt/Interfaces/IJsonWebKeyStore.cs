using eShopEnterprise.Jwt.Model;
using System.Collections.Generic;

namespace eShopEnterprise.Jwt.Interfaces
{
    public interface IJsonWebKeyStore
    {
        void Save(SecurityKeyWithPrivate securityParamteres);
        SecurityKeyWithPrivate GetCurrentKey(JsonWebKeyType jwkType);
        bool NeedsUpdate(JsonWebKeyType jsonWebKeyType);
        IReadOnlyCollection<SecurityKeyWithPrivate> Get(JsonWebKeyType jwkType, int quantity = 5);
        void Revoke(SecurityKeyWithPrivate securityKeyWithPrivate);
    }
}
