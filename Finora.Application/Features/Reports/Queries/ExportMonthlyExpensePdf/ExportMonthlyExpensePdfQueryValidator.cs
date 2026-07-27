using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyExpensePdf
{
    public class ExportMonthlyExpensePdfQueryValidator : AbstractValidator<ExportMonthlyExpensePdfQuery>
    {
        public ExportMonthlyExpensePdfQueryValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, 2100);
        }
    }
}
