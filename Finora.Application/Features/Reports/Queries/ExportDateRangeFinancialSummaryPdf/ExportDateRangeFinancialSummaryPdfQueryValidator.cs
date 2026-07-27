using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummaryPdf
{
    public class ExportDateRangeFinancialSummaryPdfQueryValidator : AbstractValidator<ExportDateRangeFinancialSummaryPdfQuery>
    {
        public ExportDateRangeFinancialSummaryPdfQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate);

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate);
        }
    }
}
