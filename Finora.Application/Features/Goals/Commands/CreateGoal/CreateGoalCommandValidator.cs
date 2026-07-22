using FluentValidation;

namespace Finora.Application.Features.Goals.Commands.CreateGoal
{
    public class CreateGoalCommandValidator : AbstractValidator<CreateGoalCommand>
    {
        public CreateGoalCommandValidator()
        {
            RuleFor(x => x.Title).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Title is required.").MaximumLength(100);

            RuleFor(x => x.Description).MaximumLength(500);

            RuleFor(x => x.TargetAmount).GreaterThan(0).WithMessage("Target amount must be greater than zero.");

            RuleFor(x => x.TargetDate).GreaterThan(DateTimeOffset.UtcNow).When(x => x.TargetDate.HasValue).WithMessage("Target date must be in the future.");

            RuleFor(x => x.GoalCategoryId).NotEmpty().WithMessage("Goal CategoryId is required.");
        }
    }
}
