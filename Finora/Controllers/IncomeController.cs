
using Finora.Application.Features.Incomes.Commands.CreateIncome;
using Finora.Application.Features.Incomes.Commands.DeleteIncome;
using Finora.Application.Features.Incomes.Commands.UpdateIncome;
using Finora.Application.Features.Incomes.Queries.GetAllIncomes;
using Finora.Application.Features.Incomes.Queries.GetIncomeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncomeCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllIncomesQuery());

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetIncomeByIdQuery(id));

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateIncomeCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _mediator.Send(new DeleteIncomeCommand(id));

            return NoContent();
        }
    }
}
