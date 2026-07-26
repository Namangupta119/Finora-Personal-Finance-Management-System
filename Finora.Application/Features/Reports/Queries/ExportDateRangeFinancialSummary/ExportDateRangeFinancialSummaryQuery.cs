using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummary
{
    public class ExportDateRangeFinancialSummaryQuery : IRequest<byte[]>
    {
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
    }
}
