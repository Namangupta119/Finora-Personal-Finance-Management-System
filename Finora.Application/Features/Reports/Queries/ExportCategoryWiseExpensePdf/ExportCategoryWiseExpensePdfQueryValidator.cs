using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpensePdf
{
    public class ExportCategoryWiseExpensePdfQueryValidator : AbstractValidator<ExportCategoryWiseExpensePdfQuery>
    {
        public ExportCategoryWiseExpensePdfQueryValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTimeOffset.UtcNow.Year);
        }
    }
}
