using System.Threading.Tasks;

namespace eShopEnterprise.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
