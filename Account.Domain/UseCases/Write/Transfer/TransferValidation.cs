using System;
using System.Collections.Generic;
using System.Text;
using Account.Domain.DomainModels.Inputs.Write;
using FluentValidation;

namespace Account.Domain.UseCases.Write.Transfer
{
    internal sealed class TransferValidation : AbstractValidator<TransferCommand>
    {
        public TransferValidation()
        {
            ValidateAccountNumberTo();
            ValidateAccountNumberFrom();
            ValidateAmount();
        }

        internal void ValidateAccountNumberTo()
        {
            RuleFor(c => c.AccountNumberTo)
                .NotEmpty()
                .WithMessage("The account number of the destination of transfer " +
                             "must be not empty");
        }
        internal void ValidateAccountNumberFrom()
        {
            RuleFor(c => c.AccountNumberFrom)
                .NotEmpty()
                .WithMessage("The account number of the source of transfer " +
                             "must be not empty");
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
