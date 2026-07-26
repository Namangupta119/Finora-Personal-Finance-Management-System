using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummary
{
    public class ExportDateRangeFinancialSummaryQueryValidator : AbstractValidator<ExportDateRangeFinancialSummaryQuery>
    {
        public ExportDateRangeFinancialSummaryQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("Start date must be less than or equal to End date.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to Start date.");
        }
    }
}
