using BankApp.Application.Commands.Transactions.DeleteTransaction;
using System.Data;
using BankApp.Application.Commands.UserAccount.LoginUser;
using BankApp.Application.Commands.UserAccount.RegisterUser;
using BankApp.Application.Queries.Transactions.GetTransactionsForAdmin;
using BankApp.Application.Queries.UserAccount.GetUserDetails;
using BankApp.Application.Services.LoggingServices;
using BankApp.Server.Common;
using BankApp.Server.Common.JWTToken;
using BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankApp.Application.Queries.UserAccount.GetUsers;
using BankApp.Application.Commands.UserAccount.DeleteUser;

namespace BankApp.Server.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
        private readonly IMediator mediator;
        private readonly JwtService jwtService;
        private Application.Services.LoggingServices.ILogger _logger { get; set; } = new ConsoleLogger();
        private LoggingService _consoleLoggingService { get; set; }


        public AccountController(IMediator mediator, JwtService jwtService)
		{
            ArgumentNullException.ThrowIfNull(mediator);
            ArgumentNullException.ThrowIfNull(jwtService);
            this.mediator = mediator;
            this.jwtService = jwtService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var registerCommand = CqrsBuilder<RegisterDto, RegisterUserCommand>.Build(registerDto);

            var result = await mediator.Send(registerCommand);

            _consoleLoggingService = new LoggingService(_logger);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(nameof(registerDto.Username), "This Username is not available.");
                return BadRequest(ModelState);
            }
            else
            {
                _consoleLoggingService.Info($"User {registerDto.Username} registered successfully.");
                return Ok(ModelState);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var loginCommand = CqrsBuilder<LoginDto, LoginUserCommand>.Build(dto);

            var result = await mediator.Send(loginCommand);

            if (!result.IsSuccessful)
                return BadRequest(result.Error);

            var userDetailsQuery = CqrsBuilder<LoginDto, GetUserDetailsQuery>.Build(dto);

            var userDetails = await mediator.Send(userDetailsQuery);

            var jwtToken = jwtService.CreateAuthToken(userDetails.Id, userDetails.Username, userDetails.Roles);

            Response.Cookies.Append(Constants.TokenCookieName, jwtToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.MaxValue
            });

            if (Response.Headers.ContainsKey("Set-Cookie"))
            {
                return Ok(jwtToken);

            }
            else
            {
                return BadRequest("Failed to append cookie");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<UserDetailsDto>> GetUsers()
        {
            var users = await mediator.Send(new GetUsersQuery());
            if (users is null)
                return new();
            var usersList = new List<UserDetailsDto>();
            foreach (var user in users)
            {
                var newUser = new UserDetailsDto
                {
                    Id = user.Id.ToString(),
                    Fullname = user.FirstName +" "+ user.LastName,
                    Email = user.Email,
                    Username = user.UserName
                };
                usersList.Add(newUser);
            }
            return usersList;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand()
            {
                Id = id
            };

            var response = await mediator.Send(command);

            return response.IsSuccessful ? Ok() : BadRequest(response.Error);
        }
    }
}

