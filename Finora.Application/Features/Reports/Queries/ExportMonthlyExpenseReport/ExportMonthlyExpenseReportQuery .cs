using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyExpenseReport
{
    public class ExportMonthlyExpenseReportQuery : IRequest<byte[]>
    {
        public int Year { get; set; }
    }
}
