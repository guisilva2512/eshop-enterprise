using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace eShopEnterprise.Identidade.API.Data
{
    public class ApplicationDbContext : IdentityDbContext, ISecurityKeyContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }
    }
}
