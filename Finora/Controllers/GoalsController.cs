using Finora.Application.Features.Goals.Commands.CreateGoal;
using Finora.Application.Features.Goals.Commands.DeleteGoal;
using Finora.Application.Features.Goals.Commands.UpdateGoal;
using Finora.Application.Features.Goals.Queries.GetAllGoals;
using Finora.Application.Features.Goals.Queries.GetGoalById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finora.Controllers
{
[ApiController]
[Route("api/[controller]")]
[Authorize]
    public class GoalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GoalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal(CreateGoalCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGoals()
        {
            var result = await _mediator.Send(new GetAllGoalsQuery());

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGoalById(Guid id)
        {
            var result = await _mediator.Send(new GetGoalByIdQuery
            {
                Id = id
            });

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGoal(Guid id, UpdateGoalCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route Id and Request Id do not match.");

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveGoal(Guid id)
        {
            var command = new DeleteGoalCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
