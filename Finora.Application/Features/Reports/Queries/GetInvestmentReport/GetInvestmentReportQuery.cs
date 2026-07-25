using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetInvestmentReport
{
    public class GetInvestmentReportQuery : IRequest<List<InvestmentReportDto>>
    {
    }
}
