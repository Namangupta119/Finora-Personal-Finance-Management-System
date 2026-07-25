using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport
{
    public class GetMonthlyExpenseReportQueryValidator : AbstractValidator<GetMonthlyExpenseReportQuery>
    {
        public GetMonthlyExpenseReportQueryValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTimeOffset.UtcNow.Year);
        }
    }
}
