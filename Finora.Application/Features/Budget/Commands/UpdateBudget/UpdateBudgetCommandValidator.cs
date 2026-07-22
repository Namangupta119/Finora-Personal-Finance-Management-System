using FluentValidation;

namespace Finora.Application.Features.Budget.Commands.UpdateBudget
{
    public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
    {
        public UpdateBudgetCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category Id is required.");

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Year).InclusiveBetween(2000, DateTimeOffset.UtcNow.Year + 10).WithMessage($"Year must be between 2000 and {DateTimeOffset.UtcNow.Year + 10}.");

            RuleFor(x => x.Month).InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
        }
    }
}
