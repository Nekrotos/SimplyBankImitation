using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain.DomainModels.Account;
using Account.Domain.DomainModels.SeedOfWork;
using Account.Infrastructure.MsSql.EntityConfigurations;
using Account.Infrastructure.MsSql.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.MsSql.Data
{
    public sealed class AccountContext : DbContext, IUnitOfWork
    {
        public AccountContext()
        {
        }

        public AccountContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountDomain> Accounts { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken token = default)
        {
            try
            {
                await SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException e)
            {
                e.Entries
                    .ToList()
                    .ForEach(entry =>
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();
                        proposedValues
                            .Properties
                            .ToList()
                            .ForEach(prop =>
                            {
                                var databaseValue = databaseValues[prop];
                                proposedValues[prop] = databaseValue;
                            });
                        entry.OriginalValues.SetValues(databaseValues);

                        throw new InfrastructureConcurrencyException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name
                        );
                    });
            }
            return true;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new AccountTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
