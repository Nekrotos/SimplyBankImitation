using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain.DomainModels.Account;
using Account.Domain.DomainModels.SeedOfWork;
using CommonLibrary.SeedOfWork;

namespace Account.Domain.InfrastructureInterfaces
{
    public interface IWrite<TDomain>
        : IRepository<TDomain>
        where TDomain : IAggregateRoot
    {
        Task<AccountDomain> Create(AccountDomain domain, CancellationToken token);
    }
}
