using Finora.Application.Features.Dashboard.GetRecentTransactions;
using Finora.Application.Features.Dashboard.Queries;
using Finora.Application.Features.Dashboard.Queries.GetExpenseAnalytics;
using Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense;
using Finora.Application.Features.Dashboard.Queries.GetRecentTransactions;
using Finora.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardSummaryDto>> GetDashboard()
        {
            var result = await _mediator.Send(new GetDashboardSummaryQuery());

            return Ok(result);
        }

        [HttpGet("recent-transactions")]
        public async Task<ActionResult<IReadOnlyList<RecentTransactionDto>>> GetRecentTransactions()
        {
            var result = await _mediator.Send(new GetRecentTransactionsQuery());

            return Ok(result);
        }

        [HttpGet("expense-analytics")]
        public async Task<ActionResult<IReadOnlyList<ExpenseAnalyticsDto>>> GetExpenseAnalytics()
        {
            var result = await _mediator.Send(new GetExpenseAnalyticsQuery());

            return Ok(result);
        }

        [HttpGet("monthly-income-expense")]
        public async Task<IActionResult> GetMonthlyIncomeExpense()
        {
            var result = await _mediator.Send(new GetMonthlyIncomeExpenseQuery());

            return Ok(result);
        }
    }
}
