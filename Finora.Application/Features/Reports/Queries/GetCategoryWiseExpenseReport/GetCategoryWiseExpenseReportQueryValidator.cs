using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport
{
    public class GetCategoryWiseExpenseReportQueryValidator : AbstractValidator<GetCategoryWiseExpenseReportQuery>
    {
        public GetCategoryWiseExpenseReportQueryValidator()
        {
            RuleFor(x => x.Year).InclusiveBetween(2000, DateTime.UtcNow.Year);
        }
    }
}
