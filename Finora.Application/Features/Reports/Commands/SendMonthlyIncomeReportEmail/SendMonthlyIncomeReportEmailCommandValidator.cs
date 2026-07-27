using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendMonthlyIncomeReportEmail
{
    public class SendMonthlyIncomeReportEmailCommandValidator : AbstractValidator<SendMonthlyIncomeReportEmailCommand>
    {
        public SendMonthlyIncomeReportEmailCommandValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTime.UtcNow.Year);
        }
    }
}
