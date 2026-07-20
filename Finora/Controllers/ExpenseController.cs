using Finora.Application.Categories.Commands.CreateCategory;
using Finora.Application.Features.Expenses.Commands.CreateExpense;
using Finora.Application.Features.Expenses.Commands.DeleteExpense;
using Finora.Application.Features.Expenses.Commands.UpdateExpense;
using Finora.Application.Features.Expenses.Queries.GetExpenseById;
using Finora.Application.Features.Expenses.Queries.GetExpenses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ExpensesDto>>> GetExpense()
        {
            var result = await _mediator.Send(new GetExpensesQuery());

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExpensesDto>> GetExpenseById(Guid id)
        {
            var result = await _mediator.Send(new GetExpenseByIdQuery(id));

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateExpense(Guid id, UpdateExpenseCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            await _mediator.Send(new DeleteExpenseCommand(id));

            return NoContent();
        }
    }
}
