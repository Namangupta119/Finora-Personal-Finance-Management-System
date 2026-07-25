
using Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport;
using Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary;
using Finora.Application.Features.Reports.Queries.GetInvestmentReport;
using Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport;
using Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finora.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("monthly-expenses")]
        public async Task<IActionResult> GetMonthlyExpenseReport([FromQuery] GetMonthlyExpenseReportQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("monthly-income")]
        public async Task<IActionResult> GetMonthlyIncomeReport([FromQuery] GetMonthlyIncomeReportQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("category-wise-expenses")]
        public async Task<IActionResult> GetCategoryWiseExpenseReport([FromQuery] GetCategoryWiseExpenseReportQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("date-range-summary")]
        public async Task<IActionResult> GetDateRangeFinancialSummary([FromQuery] GetDateRangeFinancialSummaryQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("investment-report")]
        public async Task<IActionResult> GetInvestmentReport([FromQuery] GetInvestmentReportQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
