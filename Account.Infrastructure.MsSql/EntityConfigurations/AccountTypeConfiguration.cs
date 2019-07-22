using Account.Domain.DomainModels.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.MsSql.EntityConfigurations
{
    public sealed class AccountTypeConfiguration
        : IEntityTypeConfiguration<AccountDomain>
    {
        public void Configure(EntityTypeBuilder<AccountDomain> builder)
        {
            builder
                .HasKey(acc => acc.AccountNumber);

            builder
                .Property(acc => acc.AccountNumber)
                .ValueGeneratedNever();

            builder
                .Property(p => p.RowVersion)
                .IsRowVersion();

            builder
                .Property(p => p.Balance)
                .HasColumnType("decimal(18,2)");

        }
    }
}
