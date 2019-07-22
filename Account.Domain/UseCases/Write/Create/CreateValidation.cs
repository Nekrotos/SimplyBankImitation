using System;
using System.Collections.Generic;
using System.Text;
using Account.Domain.DomainModels.Inputs.Write;
using FluentValidation;

namespace Account.Domain.UseCases.Write.Create
{
    internal sealed class CreateValidation : AbstractValidator<CreateCommand>
    {
        public CreateValidation()
        {
            ValidateAccountName();
        }

        internal void ValidateAccountName()
        {
            RuleFor(c => c.AccountName)
                .NotEmpty()
                .WithMessage("The account name must be not empty");
        }
    }
}
