using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetRecentTransactions
{
    public record GetDashboardSummaryQuery : IRequest<DashboardSummaryDto>;
}
