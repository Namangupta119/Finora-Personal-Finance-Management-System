using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary
{
    public class GetDateRangeFinancialSummaryQuery : IRequest<DateRangeFinancialSummaryDto>
    {
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
    }
}
