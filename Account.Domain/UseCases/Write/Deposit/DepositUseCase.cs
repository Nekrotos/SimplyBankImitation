using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain.DomainModels.Account;
using Account.Domain.DomainModels.Inputs.Write;
using Account.Domain.DomainModels.Outputs.Write;
using Account.Domain.Exceptions;
using Account.Domain.InfrastructureInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Account.Domain.UseCases.Write.Deposit
{
    public sealed class DepositUseCase 
        : IRequestHandler<DepositCommand,DepositOutput>
    {
        private readonly ILogger<DepositUseCase> _logger;
        private readonly IRead<AccountDomain> _read;
        private readonly IWrite<AccountDomain> _write;

        public DepositUseCase(
            ILogger<DepositUseCase> logger,
            IRead<AccountDomain> read,
            IWrite<AccountDomain> write)
        {
            _logger = logger;
            _read = read;
            _write = write;
        }

        public async Task<DepositOutput> Handle(
            DepositCommand command,
            CancellationToken token)
        {
            var validationResult = await new DepositValidation()
                .ValidateAsync(
                    command,
                    token);

            if (!validationResult.IsValid)
                return new DepositOutput(
                    null,
                    validationResult);

            var account = await _read
                .GetOne(
                    (Guid)command.AccountNumber,
                    token);

            if (account == null)
            {
                _logger.LogError("Account with {0}" +
                                 " account number doesn't exist",
                    command.AccountNumber);
                throw new AccountNotExistException(
                    $"Account with {command.AccountNumber}" +
                    $" account number doesn't exist");
            }

            account
                .Deposit((decimal)command.Amount);
            await _write
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new DepositOutput(
                account.Balance,
                validationResult);
        }
    }
}
