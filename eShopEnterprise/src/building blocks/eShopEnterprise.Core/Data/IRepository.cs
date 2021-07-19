using eShopEnterprise.Core.DomainObjects;
using System;

namespace eShopEnterprise.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
