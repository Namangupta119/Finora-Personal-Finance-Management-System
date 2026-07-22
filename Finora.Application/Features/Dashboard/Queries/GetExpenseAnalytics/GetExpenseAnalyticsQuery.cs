using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetExpenseAnalytics
{
    public record GetExpenseAnalyticsQuery : IRequest<IReadOnlyList<ExpenseAnalyticsDto>>;
}
