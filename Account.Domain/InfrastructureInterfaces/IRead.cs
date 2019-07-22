using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain.DomainModels.Account;
using Account.Domain.DomainModels.SeedOfWork;
using CommonLibrary.SeedOfWork;

namespace Account.Domain.InfrastructureInterfaces
{
    public interface IRead<TDomain> 
        : IRepository<TDomain> 
        where TDomain : IAggregateRoot
    {
        Task<List<AccountDomain>> GetList(
            List<Guid> accountNumbers = default,
            CancellationToken token = default);
        Task<AccountDomain> GetOne(
            Guid accountNumber,
            CancellationToken token = default);
        Task<bool> IsExist(
            Guid accountNumber,
            CancellationToken token = default);
        Task<(bool, List<Guid>)> AreExist(
            List<Guid> accountNumbers,
            CancellationToken token = default);
    }
}
