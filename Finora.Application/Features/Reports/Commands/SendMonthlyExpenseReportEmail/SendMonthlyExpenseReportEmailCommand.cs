using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendMonthlyExpenseReportEmail
{
    public record SendMonthlyExpenseReportEmailCommand(int Year) : IRequest;
}
