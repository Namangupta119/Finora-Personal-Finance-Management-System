using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendMonthlyExpenseReportEmail
{
    public class SendMonthlyExpenseReportEmailCommandValidator : AbstractValidator<SendMonthlyExpenseReportEmailCommand>
    {
        public SendMonthlyExpenseReportEmailCommandValidator()
        {

            RuleFor(x => x.Year)
                .GreaterThan(2000);
        }
    }
}
