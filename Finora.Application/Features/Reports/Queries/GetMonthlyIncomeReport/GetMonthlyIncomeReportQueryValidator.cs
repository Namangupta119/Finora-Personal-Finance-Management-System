using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport
{
    public class GetMonthlyIncomeReportQueryValidator : AbstractValidator<GetMonthlyIncomeReportQuery>
    {
        public GetMonthlyIncomeReportQueryValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTime.UtcNow.Year);
        }
    }
}
