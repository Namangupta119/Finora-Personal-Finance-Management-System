using Finora.Application.Features.RecurringTransactions.Commands.CreateRecurringTransaction;
using Finora.Application.Features.RecurringTransactions.Commands.DeleteRecurringTransaction;
using Finora.Application.Features.RecurringTransactions.Commands.UpdateRecurringTransaction;
using Finora.Application.Features.RecurringTransactions.Queries.GetAllRecurringTransactions;
using Finora.Application.Features.RecurringTransactions.Queries.GetRecurringTransactionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Finora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurringTransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecurringTransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecurringTransaction(CreateRecurringTransactionCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllRecurringTransactionsQuery());

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(
                new GetRecurringTransactionByIdQuery
                {
                    Id = id
                });

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateRecurringTransactionCommand command)
        {
            command.Id = id;

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteRecurringTransactionCommand
            {
                Id = id
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}