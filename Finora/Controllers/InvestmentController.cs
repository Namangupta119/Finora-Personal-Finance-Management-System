using Finora.Application.Features.Investments.Queries.GetInvestmentDashboard;
using Finora.Application.Investments.Commands.CreateInvestment;
using Finora.Application.Investments.Commands.DeleteInvestment;
using Finora.Application.Investments.Commands.UpdateInvestment;
using Finora.Application.Investments.Queries.GetInvestmentById;
using Finora.Application.Investments.Queries.GetInvestments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvestmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvestmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInvestmentCommand command)
        {
            var investmentId = await _mediator.Send(command);

            return Ok(investmentId);
        }

        [HttpGet]
        public async Task<IActionResult> GetInvestments([FromQuery] GetInvestmentsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetInvestmentByIdQuery
            {
                InvestmentId = id
            });

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id,UpdateInvestmentCommand command)
        {
            if (id != command.InvestmentId)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteInvestmentCommand
            {
                InvestmentId = id
            });

            return NoContent();
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _mediator.Send(new GetInvestmentDashboardQuery());

            return Ok(result);
        }
    }
}
