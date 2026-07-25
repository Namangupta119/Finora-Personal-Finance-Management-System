using FluentValidation;

namespace Finora.Application.Investments.Queries.GetInvestments
{
    public class GetInvestmentsQueryValidator : AbstractValidator<GetInvestmentsQuery>
    {
        public GetInvestmentsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
