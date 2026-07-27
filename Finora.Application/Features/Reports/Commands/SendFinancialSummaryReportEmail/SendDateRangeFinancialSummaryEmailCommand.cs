using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendFinancialSummaryReportEmail
{
    public record SendDateRangeFinancialSummaryEmailCommand(
    DateTimeOffset StartDate,
    DateTimeOffset EndDate) : IRequest;
}
