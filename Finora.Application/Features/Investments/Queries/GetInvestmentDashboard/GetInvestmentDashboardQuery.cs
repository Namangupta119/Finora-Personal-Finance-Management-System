using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Investments.Queries.GetInvestmentDashboard
{
    public class GetInvestmentDashboardQuery : IRequest<InvestmentDashboardDto>
    {
    }
}
