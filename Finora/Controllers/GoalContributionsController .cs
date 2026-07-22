using Finora.Application.Features.GoalContributions.Commands.AddGoalContribution;
using Finora.Application.Features.GoalContributions.Queries.GetGoalContributions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
