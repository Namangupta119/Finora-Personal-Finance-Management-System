
using Finora.Application.Features.Reports.Commands.SendCategoryExpenseReportEmail;
using Finora.Application.Features.Reports.Commands.SendFinancialSummaryReportEmail;
using Finora.Application.Features.Reports.Commands.SendInvestmentReportEmail;
using Finora.Application.Features.Reports.Commands.SendMonthlyExpenseReportEmail;
using Finora.Application.Features.Reports.Commands.SendMonthlyIncomeReportEmail;
using Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpensePdf;
using Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpenseReport;
using Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummary;
using Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummaryPdf;
using Finora.Application.Features.Reports.Queries.ExportInvestmentPdf;
using Finora.Application.Features.Reports.Queries.ExportInvestmentReport;
using Finora.Application.Features.Reports.Queries.ExportMonthlyExpensePdf;
using Finora.Application.Features.Reports.Queries.ExportMonthlyExpenseReport;
using Finora.Application.Features.Reports.Queries.ExportMonthlyIncomePdf;
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

        [HttpGet("investments/export")]
        public async Task<IActionResult> ExportInvestmentReport()
        {
            var file = await _mediator.Send(new ExportInvestmentReportQuery());

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "InvestmentReport.xlsx");
        }

        [HttpGet("monthly-expenses/pdf")]
        public async Task<IActionResult> ExportMonthlyExpensePdf([FromQuery] ExportMonthlyExpensePdfQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/pdf",
                $"MonthlyExpenseReport-{query.Year}.pdf");
        }

        [HttpGet("monthly-income/pdf")]
        public async Task<IActionResult> ExportMonthlyIncomePdf([FromQuery] ExportMonthlyIncomePdfQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/pdf",
                $"MonthlyIncomeReport-{query.Year}.pdf");
        }

        [HttpGet("category-wise-expenses/pdf")]
        public async Task<IActionResult> ExportCategoryWiseExpensePdf([FromQuery] ExportCategoryWiseExpensePdfQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/pdf",
                $"CategoryWiseExpenseReport-{query.Year}.pdf");
        }

        [HttpGet("financial-summary/pdf")]
        public async Task<IActionResult> ExportFinancialSummaryPdf([FromQuery] ExportDateRangeFinancialSummaryPdfQuery query)
        {
            var file = await _mediator.Send(query);

            return File(
                file,
                "application/pdf",
                $"FinancialSummary-{query.StartDate:yyyyMMdd}-{query.EndDate:yyyyMMdd}.pdf");
        }

        [HttpGet("investments/pdf")]
        public async Task<IActionResult> ExportInvestmentPdf()
        {
            var file = await _mediator.Send(new ExportInvestmentPdfQuery());

            return File(
                file,
                "application/pdf",
                "InvestmentReport.pdf");
        }

        [HttpPost("monthly-expense/email")]
        public async Task<IActionResult> SendMonthlyExpenseReportEmail(SendMonthlyExpenseReportEmailCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                Message = "Monthly expense report sent successfully."
            });
        }

        [HttpPost("monthly-income/email")]
        public async Task<IActionResult> SendMonthlyIncomeReportEmail([FromBody] SendMonthlyIncomeReportEmailCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                Message = "Monthly income report has been sent successfully."
            });
        }

        [HttpPost("category-expense/email")]
        public async Task<IActionResult> SendCategoryExpenseReportEmail([FromBody] SendCategoryExpenseReportEmailCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                Message = "Category-wise expense report has been sent successfully."
            });
        }

        [HttpPost("date-range-financial-summary/email")]
        public async Task<IActionResult> SendDateRangeFinancialSummaryEmail([FromBody] SendDateRangeFinancialSummaryEmailCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                Message = "Date range financial summary report has been sent successfully."
            });
        }

        [HttpPost("investment/email")]
        public async Task<IActionResult> SendInvestmentReportEmail()
        {
            await _mediator.Send(new SendInvestmentReportEmailCommand());

            return Ok(new
            {
                Message = "Investment report has been sent successfully."
            });
        }
    }
}
