namespace Account.API.Models.Views
{
    public sealed class DepositView
    {
        public DepositView(decimal? balance)
        {
            Balance = balance;
        }

        public decimal? Balance { get; }
    }
}
