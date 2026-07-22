using FluentValidation;

namespace Finora.Application.Features.Budget.Commands.CreateBudget
{
    public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
    {
        public CreateBudgetCommandValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Budget amount must be greater than zero.");

            RuleFor(x => x.Month).InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12");

            RuleFor(x => x.Year).InclusiveBetween(2000, DateTimeOffset.UtcNow.Year + 10).WithMessage("Please enter a valid year.");
        }
    }
}
