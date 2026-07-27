using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendCategoryExpenseReportEmail
{
    public class SendCategoryExpenseReportEmailCommandValidator : AbstractValidator<SendCategoryExpenseReportEmailCommand>
    {
        public SendCategoryExpenseReportEmailCommandValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTime.UtcNow.Year);
        }
    }
}
