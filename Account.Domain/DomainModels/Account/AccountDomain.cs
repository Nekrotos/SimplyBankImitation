using System;
using System.Diagnostics.CodeAnalysis;
using Account.Domain.Exceptions;
using CommonLibrary.SeedOfWork;

namespace Account.Domain.DomainModels.Account
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public sealed class AccountDomain : SeedOfWork.Domain,IAggregateRoot
    {
        #region C-tors
        public AccountDomain(Guid? userId, string accountName)
        {
            UserId = userId;
            RowVersion = default;
            AccountNumber = Guid.NewGuid();
            AccountName = accountName;
            Balance = decimal.Zero;
            DateCreated = DateTime.UtcNow;
            LastModificationDate = DateTime.UtcNow;
        }

        private AccountDomain()
        {
        }
        #endregion

        #region Props
        public Guid? UserId { get; private set; }
        public byte[] RowVersion { get; private set; }
        public new Guid AccountNumber { get; private set; }
        public string AccountName { get; private set; }
        public decimal Balance { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModificationDate { get; private set; }
        #endregion

        #region Private Methods
        public void Deposit(decimal amount)
        {
            amount = Math.Round(amount, 2);
            Balance += amount;
            LastModificationDate = DateTime.UtcNow;
        }

        public void Withdraw(decimal amount)
        {
            amount = Math.Round(amount, 2);

            if (Balance < amount)
            {
                throw new DomainException(
                    $"Сумма снятия превышает " +
                    $"остаток на счете на {Math.Abs(Balance - amount)}$");
            }

            Balance -= amount;
            LastModificationDate = DateTime.UtcNow;
        }
        #endregion

    }
}
