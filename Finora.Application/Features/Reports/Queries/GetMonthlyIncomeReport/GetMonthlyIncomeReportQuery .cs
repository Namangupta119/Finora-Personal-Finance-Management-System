using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport
{
    public class GetMonthlyIncomeReportQuery : IRequest<List<MonthlyIncomeReportDto>>
    {
        public int Year { get; set; }
    }
}
