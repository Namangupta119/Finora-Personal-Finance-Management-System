using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Queries.GetInvestmentById
{
    public class GetInvestmentByIdQueryValidator : AbstractValidator<GetInvestmentByIdQuery>
    {
        public GetInvestmentByIdQueryValidator()
        {
            RuleFor(x => x.InvestmentId)
                .NotEmpty();
        }
    }
}
