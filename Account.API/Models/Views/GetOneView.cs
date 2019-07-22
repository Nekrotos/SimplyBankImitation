using System;

namespace Account.API.Models.Views
{
    public sealed class GetOneView
    {
        public GetOneView(
            string accountName,
            Guid? accountNumber,
            decimal? balance)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public string AccountName { get; }
        public Guid? AccountNumber { get; }
        public decimal? Balance { get; }
    }
}
