using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendInvestmentReportEmail
{
    public record SendInvestmentReportEmailCommand : IRequest;
}
