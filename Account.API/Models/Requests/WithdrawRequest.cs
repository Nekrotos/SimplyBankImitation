using System;

namespace Account.API.Models.Requests
{
    public sealed class WithdrawRequest
    {
        public Guid? AccountNumber { get; set; }
        public decimal? Amount { get; set; }
    }
}
