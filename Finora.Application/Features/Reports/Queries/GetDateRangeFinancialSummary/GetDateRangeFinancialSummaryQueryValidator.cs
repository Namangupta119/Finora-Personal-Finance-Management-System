using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary
{
    public class GetDateRangeFinancialSummaryQueryValidator : AbstractValidator<GetDateRangeFinancialSummaryQuery>
    {
        public GetDateRangeFinancialSummaryQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("Start date must be less than or equal to end date.");
        }
    }
}
