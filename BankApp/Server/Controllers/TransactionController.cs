using BankApp.Application.Commands.Transactions.AddPersonalTransaction;
using BankApp.Application.Commands.Transactions.AddTransactionByEmail;
using BankApp.Application.Commands.Transactions.DeleteTransaction;
using BankApp.Application.Commands.Transactions.RepeatTransaction;
using BankApp.Application.Queries.GetTransactions;
using BankApp.Application.Queries.Transactions.GetTransactionsForAdmin;
using BankApp.Client.Pages.Transactions;
using BankApp.Server.Common;
using BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        public readonly IMediator mediator;

        public TransactionController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost("repeat")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RepeatTransaction([FromBody] TransactionDto transactionDto)
        {
            var command = CqrsBuilder<TransactionDto, RepeatTransactionCommand>.Build(transactionDto);

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpPost("add/p2p/email")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateTransactionByEmail([FromBody] TransactionByEmailDto transactionDto)
        {
            var command = CqrsBuilder<TransactionByEmailDto, AddTransactionByEmailCommand>.Build(transactionDto);

            if (command == null)
            {
                return BadRequest();
            }

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpPost("add/p2p/walletcode")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> CreateTransactionByWalletCode([FromBody] TransactionByWalletCodeDto transactionDto)
        {
            var command = CqrsBuilder<TransactionByWalletCodeDto, AddTransactionByWalletCodeCommand>.Build(transactionDto);

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpPost("add/personal")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreatePersonalTransaction([FromBody] TransactionDto transactionDto)
        {
            var command = CqrsBuilder<TransactionDto, AddPersonalTransactionCommand>.Build(transactionDto);

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<List<TransactionDto>> GetTransactions()
        {
            var userClaims = User.Claims;
            var username = userClaims.FirstOrDefault(c => c.Type == "username")?.Value;

            var transactions = await mediator.Send(new GetTransactionsQuery() { User = username });
            if (transactions is null)
                return new();
            var transactionsList = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                var newTransaction = new TransactionDto
                {
                    Id = transaction.Id.ToString(),
                    Message = transaction.Message,
                    SourceWalletId = transaction.SourceWalletId.ToString(),
                    DestinationWalletId = transaction.DestinationWalletId.ToString(),
                    TransactionAmount = transaction.TransactionAmount,

                };
                transactionsList.Add(newTransaction);
            }
            return transactionsList;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<List<TransactionDto>> GetTransactionsForAdmin()
        {
            var transactions = await mediator.Send(new GetTransactionsForAdminQuery());
            if (transactions is null)
                return new();
            var transactionsList = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                var newTransaction = new TransactionDto
                {
                    Id = transaction.Id.ToString(),
                    Message = transaction.Message,
                    SourceWalletId = transaction.SourceWalletId.ToString(),
                    DestinationWalletId = transaction.DestinationWalletId.ToString(),
                    TransactionAmount = transaction.TransactionAmount,

                };
                transactionsList.Add(newTransaction);
            }
            return transactionsList;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTransaction(string id)
        {
            var command = new DeleteTransactionCommand()
            {
                Id = id
            };

            var response = await mediator.Send(command);

            return response.IsSuccessful ? Ok() : BadRequest(response.Error);
        }
    }
}

