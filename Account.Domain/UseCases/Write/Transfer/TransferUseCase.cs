using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain.DomainModels.Account;
using Account.Domain.DomainModels.Inputs.Write;
using Account.Domain.DomainModels.Outputs.Write;
using Account.Domain.Exceptions;
using Account.Domain.InfrastructureInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Account.Domain.UseCases.Write.Transfer
{
    public sealed class TransferUseCase 
        : IRequestHandler<TransferCommand,TransferOutput>
    {
        private readonly ILogger<TransferUseCase> _logger;
        private readonly IRead<AccountDomain> _read;
        private readonly IWrite<AccountDomain> _write;

        public TransferUseCase(
            ILogger<TransferUseCase> logger,
            IRead<AccountDomain> read,
            IWrite<AccountDomain> write)
        {
            _logger = logger;
            _read = read;
            _write = write;
        }

        public async Task<TransferOutput> Handle(
            TransferCommand command,
            CancellationToken token)
        {
            var validationResult = await new TransferValidation()
                .ValidateAsync(
                    command,
                    token);

            if (!validationResult.IsValid)
                return new TransferOutput(
                    null,
                    null,
                    validationResult);

            var idsForCheck = new List<Guid>
            {
                (Guid)command.AccountNumberFrom,
                (Guid)command.AccountNumberTo
            };
            var (areExist, ids) = await _read
                .AreExist(
                    idsForCheck,
                    token);

            if (!areExist)
            {
                _logger.LogError($"Accounts with " +
                                 $"{ids.Select(s=>s)} " +
                                 "account numbers doesn't exist");
                throw new AccountNotExistException(
                    $"Accounts with " +
                    $"{ids.Select(s => s.ToString())}" +
                    " accounts number doesn't exist");
            }

            var accounts = await _read
                .GetList(
                    idsForCheck,
                    token);
            accounts.ForEach(ac =>
            {
                if (ac.AccountNumber 
                    == command.AccountNumberFrom) ac.Withdraw((decimal)command.Amount);
                ac.Deposit((decimal)command.Amount);
            });

            await _write
                .UnitOfWork
                .SaveEntitiesAsync(token);
            var sourceAccount = accounts
                .FirstOrDefault(ac => ac.AccountNumber == command.AccountNumberFrom);
            return new TransferOutput(
                sourceAccount.AccountNumber,
                sourceAccount.Balance,
                validationResult);
        }
    }
}
