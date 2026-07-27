using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendFinancialSummaryReportEmail
{
    public class SendDateRangeFinancialSummaryEmailCommandValidator : AbstractValidator<SendDateRangeFinancialSummaryEmailCommand>
    {
        public SendDateRangeFinancialSummaryEmailCommandValidator()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("Start date must be before or equal to end date.");
        }
    }
}
