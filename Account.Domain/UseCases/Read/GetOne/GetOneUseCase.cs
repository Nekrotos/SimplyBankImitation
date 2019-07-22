using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain.DomainModels.Account;
using Account.Domain.DomainModels.Inputs.Read;
using Account.Domain.DomainModels.Outputs.Read;
using Account.Domain.Exceptions;
using Account.Domain.InfrastructureInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Account.Domain.UseCases.Read.GetOne
{
    public sealed class GetOneUseCase 
        : IRequestHandler<GetOneQuery,GetOneOutput>
    {
        private readonly ILogger<GetOneUseCase> _logger;
        private readonly IRead<AccountDomain> _read;

        public GetOneUseCase(
            ILogger<GetOneUseCase> logger,
            IRead<AccountDomain> read)
        {
            _logger = logger;
            _read = read;
        }

        public async Task<GetOneOutput> Handle(
            GetOneQuery query,
            CancellationToken token)
        {
            var validationResult =  await new GetOneValidation()
                .ValidateAsync(query,token);

            if (!validationResult.IsValid)
                return new GetOneOutput(
                    null,
                    null,
                    null,
                    validationResult);

            var account = await _read.GetOne(
                (Guid)query.AccountNumber,
                token);

            if (account == null)
            {
                _logger.LogError("Account with {0}" +
                                 " account number doesn't exist",
                    query.AccountNumber);
                throw new AccountNotExistException(
                    $"Account with {query.AccountNumber}" +
                    $" account number doesn't exist");
            }

            return new GetOneOutput(
                account.AccountName,
                account.AccountNumber,
                account.Balance,
                validationResult);
        }
    }
}
