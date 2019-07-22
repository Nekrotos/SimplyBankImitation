using System;
using System.Collections.Generic;
using System.Text;
using Account.Domain.DomainModels.Inputs.Write;
using FluentValidation;

namespace Account.Domain.UseCases.Write.Deposit
{
    internal sealed class DepositValidation : AbstractValidator<DepositCommand>
    {
        public DepositValidation()
        {
            ValidateAccountNumber();
            ValidateAmount();
        }

        internal void ValidateAccountNumber()
        {
            RuleFor(c => c.AccountNumber)
                .NotEmpty()
                .WithMessage("The account number must be not empty");
        }
        internal void ValidateAmount()
        {
            RuleFor(c => c.Amount)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("The amount must be not empty and greeter than 0.00");
        }
    }
}
