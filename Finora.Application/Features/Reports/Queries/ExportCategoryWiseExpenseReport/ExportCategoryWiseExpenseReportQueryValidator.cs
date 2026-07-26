using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpenseReport
{
    public class ExportCategoryWiseExpenseReportQueryValidator : AbstractValidator<ExportCategoryWiseExpenseReportQuery>
    {
        public ExportCategoryWiseExpenseReportQueryValidator()
        {
            RuleFor(x => x.Year).InclusiveBetween(2000, DateTime.UtcNow.Year).WithMessage($"Year must be between 2000 and {DateTime.UtcNow.Year}.");
        }
    }
}
