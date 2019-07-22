using FluentValidation.Results;

namespace Account.Domain.DomainModels.Outputs.Write
{
    public sealed class WithdrawOutput
    {
        public WithdrawOutput(
            decimal? balance, 
            ValidationResult validationResult)
        {
            Balance = balance;
            ValidationResult = validationResult;
        }

        public decimal? Balance { get; }
        public ValidationResult ValidationResult { get; }
    }
}
