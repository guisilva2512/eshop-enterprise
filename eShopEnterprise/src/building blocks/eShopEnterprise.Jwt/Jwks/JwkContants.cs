using eShopEnterprise.Jwt.Model;

namespace eShopEnterprise.Jwt.Jwks
{
    public class JwkContants
    {
        public static string CurrentJwkCache(JsonWebKeyType jwkType) => $"NETDEVPACK-CURRENT-{jwkType}-SECURITY-KEY";
        public const string JwksCache = "NETDEVPACK-JWKS";
    }
}
