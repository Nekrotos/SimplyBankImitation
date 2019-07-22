using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Account.API.Models.Requests;
using Account.API.Models.Views;
using Account.Domain.DomainModels.Inputs.Read;
using Account.Domain.DomainModels.Inputs.Write;
using CommonLibrary.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Account.API.Controllers
{
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMediator _mediator;

        public AccountsController(
            ILogger<AccountsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("getOne")]
        public async Task<IActionResult> GetOne(
            [FromBody] GetOneRequest request,
            CancellationToken token)
        {
            var query = new GetOneQuery(
                request.AccountNumber,
                CorrelationContext.Get());
            var response = await _mediator
                .Send(
                    query,
                    token);

            if (!response
                .ValidationResult
                .IsValid)
            {
                var errors = string.Empty;
                response
                    .ValidationResult
                    .Errors
                    .ToList()
                    .ForEach(e => { errors += $"{e}//r//n"; });
                return BadRequest(errors);
            }

            return Ok(new GetOneView(
                response.AccountName,
                response.AccountNumber,
                response.Balance));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount(
            [FromBody] CreateAccountRequest request,
            CancellationToken token)
        {
            var query = new CreateCommand(
                request.AccountName,
                CorrelationContext.Get());
            var response = await _mediator
                .Send(
                    query,
                    token);

            if (!response
                .ValidationResult
                .IsValid)
            {
                var errors = string.Empty;
                response
                    .ValidationResult
                    .Errors
                    .ToList()
                    .ForEach(e => { errors += $"{e}//r//n"; });
                return BadRequest(errors);
            }

            return Ok(new CreateAccountView(
                response.AccountName,
                response.AccountNumber,
                response.Balance));
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(
            [FromBody] DepositRequest request,
            CancellationToken token)
        {
            var query = new DepositCommand(
                request.AccountNumber,
                request.Amount,
                CorrelationContext.Get());
            var response = await _mediator
                .Send(
                    query,
                    token);

            if (!response
                .ValidationResult
                .IsValid)
            {
                var errors = string.Empty;
                response
                    .ValidationResult
                    .Errors
                    .ToList()
                    .ForEach(e => { errors += $"{e}//r//n"; });
                return BadRequest(errors);
            }

            return Ok(new DepositView(
                response.Balance));
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(
            [FromBody] WithdrawRequest request,
            CancellationToken token)
        {
            var query = new WithdrawCommand(
                request.AccountNumber,
                request.Amount,
                CorrelationContext.Get());
            var response = await _mediator
                .Send(
                    query,
                    token);

            if (!response
                .ValidationResult
                .IsValid)
            {
                var errors = string.Empty;
                response
                    .ValidationResult
                    .Errors
                    .ToList()
                    .ForEach(e => { errors += $"{e}//r//n"; });
                return BadRequest(errors);
            }

            return Ok(new WithdrawView(
                response.Balance));
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(
            [FromBody] TransferRequest request,
            CancellationToken token)
        {
            var query = new TransferCommand(
                request.AccountNumberFrom,
                request.AccountNumberTo,
                request.Amount,
                CorrelationContext.Get());
            var response = await _mediator
                .Send(
                    query,
                    token);

            if (!response
                .ValidationResult
                .IsValid)
            {
                var errors = string.Empty;
                response
                    .ValidationResult
                    .Errors
                    .ToList()
                    .ForEach(e => { errors += $"{e}//r//n"; });
                return BadRequest(errors);
            }

            return Ok(new TransferView(
                response.AccountNumberTo,
                response.Balance));
        }
    }
}
