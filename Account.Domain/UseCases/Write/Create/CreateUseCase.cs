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

namespace Account.Domain.UseCases.Write.Create
{
    public sealed class CreateUseCase 
        : IRequestHandler<CreateCommand,CreateOutput>
    {
        private readonly ILogger<CreateUseCase> _logger;
        private readonly IRead<AccountDomain> _read;
        private readonly IWrite<AccountDomain> _write;

        public CreateUseCase(
            ILogger<CreateUseCase> logger,
            IRead<AccountDomain> read,
            IWrite<AccountDomain> write)
        {
            _logger = logger;
            _read = read;
            _write = write;
        }

        public async Task<CreateOutput> Handle(
            CreateCommand command,
            CancellationToken token)
        {
            var validationResult = await new CreateValidation()
                .ValidateAsync(
                    command,
                    token);

            if (!validationResult.IsValid)
                return new CreateOutput(
                    null,
                    null,
                    null,
                    validationResult);

            var response = await _write
                .Create(
                    new AccountDomain(
                        null,
                        command.AccountName),
                    token);
            await _write.UnitOfWork.SaveEntitiesAsync(token);

            return new CreateOutput(
                response.AccountName,
                response.AccountNumber,
                response.Balance,
                validationResult);
        }
    }
}
