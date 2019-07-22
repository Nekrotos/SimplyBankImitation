using System;
using Account.Domain.DomainModels.Outputs.Write;
using CommonLibrary.Messages;

namespace Account.Domain.DomainModels.Inputs.Write
{
    public sealed class CreateCommand : Command<CreateOutput>
    {
        public CreateCommand(
            string accountName,
            string correlationId)
        {
            AccountName = accountName;
            CorrelationId = correlationId
                            ?? (CorrelationId = Guid.NewGuid()
                                .ToString());
        }

        public string AccountName { get; }

    }
}
