using BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankApp.Application.Commands.AddCommision;
using BankApp.Server.Common;
using BankApp.Application.Queries.GetCommisions;
using BankApp.Application.Commands.UpdateCommision;
using BankApp.Application.Commands.DeleteCommision;
using BankApp.Domain.Models;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
    [Route("api/commisions")]
    [ApiController]
    public class CommisionController : ControllerBase
    {
        public readonly IMediator mediator;

        public CommisionController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCommision([FromBody] CommisionDto dto)
        {
            var command = CqrsBuilder<CommisionDto, AddCommisionCommand>.Build(dto);

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IEnumerable<CommisionDto>> GetCommisions()
        {
            var commisions = await mediator.Send(new GetCommisionsQuery());
            var commisionsList = new List<CommisionDto>();
            foreach (var commision in commisions)
            {
                var newCommision = new CommisionDto
                {
                    Id = commision.Id.ToString(),
                    WalletType = commision.WalletType,
                    CommisionRate = commision.CommisionRate
                };
                commisionsList.Add(newCommision);
            }
            return commisionsList;
        }

        [HttpPut("{id}")]
        [Route("update")]
        public async Task<IActionResult> UpdateCommision(string id, [FromBody] CommisionDto dto)
        {
            var command = CqrsBuilder<CommisionDto, UpdateCommisionCommand>.Build(dto);

            var commisionToUpdate = await mediator.Send(command);

            return commisionToUpdate.IsSuccessful ? Ok() : BadRequest(commisionToUpdate.Error);
        }

        [HttpDelete("{commissionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCommision(string commissionId)
        {
            var command = new DeleteCommisionCommand()
            {
                Id = commissionId
            };

            var commisionToDelete = await mediator.Send(command);

            return commisionToDelete.IsSuccessful ? Ok() : BadRequest(commisionToDelete.Error);
        }
    }
}

