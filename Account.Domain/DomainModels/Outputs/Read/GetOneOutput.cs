using System;
using FluentValidation.Results;

namespace Account.Domain.DomainModels.Outputs.Read
{
    public sealed class GetOneOutput
    {
        public GetOneOutput(
            string accountName,
            Guid? accountNumber,
            decimal? balance,
            ValidationResult validationResult)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            Balance = balance;
            ValidationResult = validationResult;
        }

        public string AccountName { get; }
        public Guid? AccountNumber { get; }
        public decimal? Balance { get; }
        public ValidationResult ValidationResult { get; }
    }
}
