using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyIncomeReport
{
    public class ExportMonthlyIncomeReportQuery : IRequest<byte[]>
    {
        public int Year { get; set; }
    }
}
