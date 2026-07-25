using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport
{
    public class GetMonthlyExpenseReportQuery : IRequest<List<MonthlyExpenseReportDto>>
    {
        public int Year { get; set; }
    }
}
