using System;
using System.Collections.Generic;
using System.Text;
using Account.Domain.DomainModels.Inputs.Read;
using FluentValidation;

namespace Account.Domain.UseCases.Read.GetOne
{
    internal sealed class GetOneValidation : AbstractValidator<GetOneQuery>
    {
        public GetOneValidation()
        {
            ValidateAccountNumber();
        }

        internal void ValidateAccountNumber()
        {
            RuleFor(c => c.AccountNumber)
                .NotEmpty()
                .WithMessage("The account number must be not empty");
        }
    }
}