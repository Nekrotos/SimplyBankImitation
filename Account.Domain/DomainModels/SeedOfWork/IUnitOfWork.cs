using System;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Domain.DomainModels.SeedOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken token = default);
    }
}
