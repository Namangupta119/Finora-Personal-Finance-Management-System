
using Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpenseReport;
using Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummary;
using Finora.Application.Features.Reports.Queries.ExportMonthlyExpenseReport;
using Finora.Application.Features.Reports.Queries.ExportMonthlyIncomeReport;
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

        [HttpGet("monthly-expenses/export")]
        public async Task<IActionResult> ExportMonthlyExpenseReport([FromQuery] ExportMonthlyExpenseReportQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"MonthlyExpenseReport-{query.Year}.xlsx");
        }

        [HttpGet("monthly-income/export")]
        public async Task<IActionResult> ExportMonthlyIncomeReport([FromQuery] ExportMonthlyIncomeReportQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"MonthlyIncomeReport-{query.Year}.xlsx");
        }

        [HttpGet("category-wise-expenses/export")]
        public async Task<IActionResult> ExportCategoryWiseExpenseReport([FromQuery] ExportCategoryWiseExpenseReportQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"CategoryWiseExpenseReport-{query.Year}.xlsx");
        }

        [HttpGet("date-range-summary/export")]
        public async Task<IActionResult> ExportDateRangeFinancialSummary([FromQuery] ExportDateRangeFinancialSummaryQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"DateRangeFinancialSummary-{query.StartDate:yyyyMMdd}-{query.EndDate:yyyyMMdd}.xlsx");
        }
    }
}
