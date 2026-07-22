using Finora.Application.Features.Budget.Commands.CreateBudget;
using Finora.Application.Features.Budget.Commands.DeleteBudget;
using Finora.Application.Features.Budget.Commands.UpdateBudget;
using Finora.Application.Features.Budget.Queries.GetBudgetById;
using Finora.Application.Features.Budget.Queries.GetBudgets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget(CreateBudgetCommand command)
        {
            var budgetId = await _mediator.Send(command);

            return Ok(budgetId);
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var result = await _mediator.Send(new GetBudgetsQuery());

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            var result = await _mediator.Send(new GetBudgetByIdQuery(id));

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateBudgetCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route id and request id do not match.");

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveBudget(Guid id)
        {
            await _mediator.Send(new DeleteBudgetCommand(id));

            return NoContent();
        }
    }
}
