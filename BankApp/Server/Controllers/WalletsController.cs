using BankApp.Application.Commands.DeleteCommision;
using BankApp.Application.Commands.ToggleFavoriteWallet;
using BankApp.Application.Commands.Wallets.AddWallet;
using BankApp.Application.Commands.Wallets.DeleteWallet;
using BankApp.Application.Queries.Wallets.GetUserWallets;
using BankApp.Application.Queries.Wallets.GetWalletById;
using BankApp.Domain.Models;
using BankApp.Infrastructure.Adapters;
using BankApp.Server.Common;
using BankApp.Server.Common.JWTToken;
using BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletsController : ControllerBase
	{
        public readonly IMediator mediator;
        private readonly IJwtService jwtService;
        private readonly IAdapter<WalletDto, Wallet> _adapter;

        public WalletsController(IMediator mediator, IJwtService jwtService, IAdapter<WalletDto, Wallet> adapter)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            ArgumentNullException.ThrowIfNull(jwtService);

            this.mediator = mediator;
            this.jwtService = jwtService;
            _adapter = adapter;
        }

        [HttpPost("add")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddWallet([FromBody] WalletDto createWalletDTO)
        {
            var command = CqrsBuilder<WalletDto, AddWalletCommand>.Build(createWalletDTO);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);

            if (userIdClaim is null)
                return BadRequest();

            var userId = userIdClaim.Value;

            command.UserId = userId;

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<WalletDto>> GetWallets()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);

            if (userIdClaim is null)
                return new List<WalletDto>();

            var userId = userIdClaim.Value;

            var query = new GetUserWalletsQuery()
            {
                UserId = userId,
            };

            var wallets = await mediator.Send(query);


            return Mapper.Map(wallets);
        }

        [HttpGet("{walletId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<WalletDto>> GetWalletById(Guid walletId)
        {
            var query = new GetWalletByIdQuery()
            {
                Id = walletId.ToString()
            };

            var wallet = await mediator.Send(query);

            if (wallet is null)
                return NotFound();

            return Ok(_adapter.ToDto(wallet));
        }

        [HttpDelete("{walletId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteWallet(string walletId)
        {
            var command = new DeleteWalletCommand()
            {
                Id = walletId
            };

            var response = await mediator.Send(command);

            return response.IsSuccessful ? Ok() : BadRequest(response.Error);
        }

        [HttpPost("toggle-favorite")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ToggleFavorite([FromBody] WalletDto wallet)
        {
            var command = new ToggleFavoriteWalletCommand()
            {
                Id = wallet.Id
            };

            var response = await mediator.Send(command);

            return response.IsSuccessful ? Ok() : BadRequest(response.Error);
        }
    }
}

