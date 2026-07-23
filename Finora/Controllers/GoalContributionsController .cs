using Finora.Application.Features.GoalContributions.Commands.AddGoalContribution;
using Finora.Application.Features.GoalContributions.Commands.DeleteGoalContribution;
using Finora.Application.Features.GoalContributions.Commands.UpdateGoalContribution;
using Finora.Application.Features.GoalContributions.Queries.GetGoalContributions;
using Finora.Application.Features.Goals.Commands.UpdateGoal;
using Finora.Application.Features.Incomes.Commands.DeleteIncome;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Finora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GoalContributionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GoalContributionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddGoalContribution(AddGoalContributionCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{goalId:guid}/contributions")]
        public async Task<IActionResult> GetGoalContributions(Guid goalId)
        {
            var result = await _mediator.Send(new GetGoalContributionsQuery
            {
                GoalId = goalId
            });

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGoalContribution(Guid id, UpdateGoalContributionCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route Id and Request Id do not match.");

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveGoalContribution(Guid id)
        {
            var command = new DeleteGoalContributionCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
