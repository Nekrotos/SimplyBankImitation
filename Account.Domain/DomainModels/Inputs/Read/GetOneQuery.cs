using System;
using Account.Domain.DomainModels.Outputs.Read;
using CommonLibrary.Messages;

namespace Account.Domain.DomainModels.Inputs.Read
{
    public sealed class GetOneQuery : Command<GetOneOutput>
    {
        public GetOneQuery(
            Guid? accountNumber,
            string correlationId = default)
        {
            AccountNumber = accountNumber;
            CorrelationId = correlationId
                            ?? (CorrelationId = Guid.NewGuid()
                                .ToString());
        }

        public Guid? AccountNumber { get; }
    }
}
