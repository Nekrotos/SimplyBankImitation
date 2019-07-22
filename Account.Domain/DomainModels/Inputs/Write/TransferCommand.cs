using System;
using Account.Domain.DomainModels.Outputs.Write;
using CommonLibrary.Messages;

namespace Account.Domain.DomainModels.Inputs.Write
{
    public sealed class TransferCommand : Command<TransferOutput>
    {
        public TransferCommand(
            Guid? accountNumberFrom,
            Guid? accountNumberTo,
            decimal? amount,
            string correlationId = default)
        {
            AccountNumberFrom = accountNumberFrom;
            AccountNumberTo = accountNumberTo;
            Amount = amount;
            CorrelationId = correlationId
                            ?? (CorrelationId = Guid.NewGuid()
                                .ToString());
        }

        public Guid? AccountNumberFrom { get; }
        public Guid? AccountNumberTo { get; }
        public decimal? Amount { get; }
    }
}
