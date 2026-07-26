using FluentValidation;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyIncomeReport
{
    public class ExportMonthlyIncomeReportQueryValidator : AbstractValidator<ExportMonthlyIncomeReportQuery>
    {
        public ExportMonthlyIncomeReportQueryValidator()
        {
            RuleFor(x => x.Year).InclusiveBetween(2000, DateTime.UtcNow.Year).WithMessage($"Year must be between 2000 and {DateTime.UtcNow.Year}."); ;
        }
    }
}
