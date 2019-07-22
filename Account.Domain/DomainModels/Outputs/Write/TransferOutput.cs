using System;
using FluentValidation.Results;

namespace Account.Domain.DomainModels.Outputs.Write
{
    public sealed class TransferOutput
    {
        public TransferOutput(
            Guid? accountNumberFrom,
            decimal? balance,
            ValidationResult validationResult)
        {
            AccountNumberFrom = accountNumberFrom;
            Balance = balance;
            ValidationResult = validationResult;
        }

        public Guid? AccountNumberFrom { get; }
        public decimal? Balance { get; }
        public ValidationResult ValidationResult { get; }
    }
}
