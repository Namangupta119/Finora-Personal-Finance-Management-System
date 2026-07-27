using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendMonthlyIncomeReportEmail
{
    public record SendMonthlyIncomeReportEmailCommand(
    int Year) : IRequest;
}
