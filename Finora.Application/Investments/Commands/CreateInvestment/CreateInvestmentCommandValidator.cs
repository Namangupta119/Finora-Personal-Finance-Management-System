using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Commands.CreateInvestment
{
    public class CreateInvestmentValidator : AbstractValidator<CreateInvestmentCommand>
    {
        public CreateInvestmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Symbol)
                .MaximumLength(20);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);

            RuleFor(x => x.PurchasePrice)
                .GreaterThan(0);

            RuleFor(x => x.CurrentPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PurchaseDate)
                .LessThanOrEqualTo(DateTimeOffset.UtcNow);

            RuleFor(x => x.Broker)
                .MaximumLength(100);

            RuleFor(x => x.Notes)
                .MaximumLength(1000);
        }
    }
}
