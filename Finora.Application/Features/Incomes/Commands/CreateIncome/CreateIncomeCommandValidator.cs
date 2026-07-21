using FluentValidation;

namespace Finora.Application.Features.Incomes.Commands.CreateIncome
{
    public class CreateIncomeCommandValidator : AbstractValidator<CreateIncomeCommand>
    {
        public CreateIncomeCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Description).MaximumLength(500);

            RuleFor(x => x.Amount).GreaterThan(0);

            RuleFor(x => x.IncomeDate).NotEmpty().LessThanOrEqualTo(DateTimeOffset.Now)
            .WithMessage("Income date cannot be in the future.");
        }
    }
}
