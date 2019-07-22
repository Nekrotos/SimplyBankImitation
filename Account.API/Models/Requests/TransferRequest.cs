using System;

namespace Account.API.Models.Requests
{
    public class TransferRequest
    {
        public Guid? AccountNumberFrom { get; set; }
        public Guid? AccountNumberTo { get; set; }
        public decimal? Amount { get; set; }
    }
}
