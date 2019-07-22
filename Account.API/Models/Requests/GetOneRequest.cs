using System;

namespace Account.API.Models.Requests
{
    public sealed class GetOneRequest
    {
        public Guid? AccountNumber { get; set; }
    }
}
