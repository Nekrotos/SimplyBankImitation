namespace Account.API.Models.Views
{
    public sealed class WithdrawView
    {
        public WithdrawView(decimal? balance)
        {
            Balance = balance;
        }

        public decimal? Balance { get; }
    }
}
