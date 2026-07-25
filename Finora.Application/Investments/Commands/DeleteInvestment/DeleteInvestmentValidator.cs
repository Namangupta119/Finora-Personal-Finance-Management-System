using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Commands.DeleteInvestment
{
    public class DeleteInvestmentValidator : AbstractValidator<DeleteInvestmentCommand>
    {
        public DeleteInvestmentValidator()
        {
            RuleFor(x => x.InvestmentId)
                .NotEmpty();
        }
    }
}
