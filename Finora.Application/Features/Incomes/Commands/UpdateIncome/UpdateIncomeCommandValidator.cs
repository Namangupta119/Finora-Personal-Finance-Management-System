using FluentValidation;

namespace Finora.Application.Features.Incomes.Commands.UpdateIncome
{
    public class UpdateIncomeCommandValidator : AbstractValidator<UpdateIncomeCommand>
    {
        public UpdateIncomeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Description).MaximumLength(500);

            RuleFor(x => x.Amount).GreaterThan(0);

            RuleFor(x => x.IncomeDate).LessThanOrEqualTo(DateTimeOffset.Now).WithMessage("Income date cannot be in the future.");
        }
    }
}
