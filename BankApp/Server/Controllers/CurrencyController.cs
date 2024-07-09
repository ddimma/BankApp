using BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankApp.Application.Commands.AddCurrency;
using BankApp.Server.Common;
using BankApp.Domain.Models;
using BankApp.Application.Queries.GetCurrencies;
using BankApp.Application.Queries.GetCurrencyById;
using BankApp.Application.Commands.UpdateCurrency;
using BankApp.Application.Commands.DeteleCurrency;

namespace BankApp.Server.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public readonly IMediator mediator;

        public CurrencyController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCurrency([FromBody] CurrencyDto dto)
        {
            var command = CqrsBuilder<CurrencyDto, AddCurrencyCommand>.Build(dto);

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<List<CurrencyDto>> GetCurrencies()
        {
            var currencies = await mediator.Send(new GetCurrenciesQuery());
            var currenciesList = new List<CurrencyDto>();
            foreach (var currency in currencies)
            {
                var newCurrency = new CurrencyDto
                {
                    Id = currency.Id.ToString(),
                    Name = currency.Name,
                    ChangeRate = currency.ChangeRate,
                    CurrencyCode = currency.CurrencyCode

                };

                currenciesList.Add(newCurrency);
            }
            //var result = Mapper<Currency, CurrencyDto>.Map(currencies).ToList();

            //var newCurrency = Mapper<Currency, CurrencyDto>.Map(currencies).ToList();
            return currenciesList;
        }

        [HttpGet("{currencyId}")]
        [Route("getById")]
        public async Task<CurrencyDto> GetCurrencyById(string currencyId)
        {
            var currency = await mediator.Send(new GetCurrencyByIdQuery
            {
                Id = currencyId
            });

            if (currency == null)
            {
                return new CurrencyDto();
            }

            var currencyDto = new CurrencyDto
            {
                Id = currency.Id.ToString(),
                Name = currency.Name,
                ChangeRate = currency.ChangeRate,
                CurrencyCode = currency.CurrencyCode

            };

            return currencyDto;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCurrency(string id, [FromBody] CurrencyDto currencyDto)
        {
            var command = CqrsBuilder<CurrencyDto, UpdateCurrencyCommand>.Build(currencyDto);
            var result = await mediator.Send(command);
            return result.IsSuccessful ? Ok(currencyDto) : BadRequest(result.Error);
        }

        [HttpPost("{currencyId}")]
        [Route("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCurrency(string currencyId)
        {
            var currencyToDelete = await mediator.Send(new DeleteCurrencyCommand
            {
                Id = currencyId
            });

            return currencyToDelete.IsSuccessful ? Ok() : BadRequest(currencyToDelete.Error);
        }
    }
}

