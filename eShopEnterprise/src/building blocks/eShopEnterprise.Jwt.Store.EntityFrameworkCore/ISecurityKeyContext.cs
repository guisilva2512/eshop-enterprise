using eShopEnterprise.Jwt.Model;
using Microsoft.EntityFrameworkCore;

namespace eShopEnterprise.Jwt.Store.EntityFrameworkCore
{
    public interface ISecurityKeyContext
    {
        /// <summary>
        /// A collection of <see cref="T:NetDevPack.Security.JwtSigningCredentials.SecurityKeyWithPrivate" />
        /// </summary>
        DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }
    }
}
