using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyIncomePdf
{
    public class ExportMonthlyIncomePdfQueryValidator : AbstractValidator<ExportMonthlyIncomePdfQuery>
    {
        public ExportMonthlyIncomePdfQueryValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, 2100);
        }
    }
}
