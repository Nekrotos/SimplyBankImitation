using System;

namespace Account.API.Models.Views
{
    public sealed class TransferView
    {
        public TransferView(
            Guid? accountNumberFrom,
            decimal? balance)
        {
            AccountNumberFrom = accountNumberFrom;
            Balance = balance;
        }

        public Guid? AccountNumberFrom { get; }
        public decimal? Balance { get; }
    }
}
