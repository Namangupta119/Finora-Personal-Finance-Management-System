using FluentValidation;

namespace Finora.Application.Features.Goals.Commands.DeleteGoal
{
    public class DeleteGoalValidator : AbstractValidator<DeleteGoalCommand>
    {
        public DeleteGoalValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Goal Id is required.");
        }
    }
}
