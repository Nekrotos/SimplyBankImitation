using System;
using Account.Domain.DomainModels.Outputs.Write;
using CommonLibrary.Messages;

namespace Account.Domain.DomainModels.Inputs.Write
{
    public sealed class DepositCommand : Command<DepositOutput>
    {
        public DepositCommand(
            Guid? accountNumber,
            decimal? amount,
            string correlationId = default)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            CorrelationId = correlationId
                            ?? (CorrelationId = Guid.NewGuid()
                                .ToString());
        }
        public Guid? AccountNumber { get; }
        public decimal? Amount { get; }
    }
}
