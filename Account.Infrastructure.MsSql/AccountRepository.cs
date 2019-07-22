using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain.DomainModels.Account;
using Account.Domain.DomainModels.SeedOfWork;
using Account.Domain.InfrastructureInterfaces;
using Account.Infrastructure.MsSql.Data;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.MsSql
{
    public sealed class AccountRepository 
        : IRead<AccountDomain>, IWrite<AccountDomain>
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<AccountDomain>> GetList(
            List<Guid> accountNumbers = default,
            CancellationToken token = default)
        {
            return await _context
                .Accounts
                .Where(ac => accountNumbers
                    .Contains(ac.AccountNumber))
                .ToListAsync(token);
        }

        public async Task<AccountDomain> GetOne(
            Guid accountNumber,
            CancellationToken token = default)
        {
            var t =  await _context
                .Accounts
                .FindAsync(
                    accountNumber);
            return t;
        }

        public async Task<bool> IsExist(
            Guid accountNumber,
            CancellationToken token = default)
        {
            return await _context
                       .Accounts
                       .AnyAsync(ac => 
                               ac.AccountNumber == accountNumber,
                           token);
        }

        public async Task<(bool, List<Guid>)> AreExist(
            List<Guid> accountNumbers, 
            CancellationToken token = default)
        {
            var accounts = await _context
                .Accounts
                .Where(ac => accountNumbers
                    .Contains(ac.AccountNumber))
                .ToListAsync(token);
            var ids = accountNumbers
                .Except(accounts
                    .Select(ac => ac.AccountNumber)
                    .ToList())
                .ToList();
            return (
                !ids.Any(), 
                ids.Any() 
                    ? ids 
                    : accountNumbers);
        }

        public async Task<AccountDomain> Create(
            AccountDomain domain,
            CancellationToken token)
        {
            var entityEntry = await _context
                .Accounts
                .AddAsync(
                    domain,
                    token);
            return entityEntry
                .Entity;
        }

        public AccountDomain Update(AccountDomain domain)
        {
            return _context
                .Update(domain)
                .Entity;
        }
    }
}
