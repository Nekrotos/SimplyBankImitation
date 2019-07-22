using System;

namespace Account.API.Models.Requests
{
    public sealed class DepositRequest
    {
        public Guid? AccountNumber { get; set; }
        public decimal? Amount { get; set; }
    }
}
