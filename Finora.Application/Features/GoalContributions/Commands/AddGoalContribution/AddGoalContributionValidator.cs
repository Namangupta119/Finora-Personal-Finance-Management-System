using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.GoalContributions.Commands.AddGoalContribution
{
    public class AddGoalContributionValidator : AbstractValidator<AddGoalContributionCommand>
    {
        public AddGoalContributionValidator()
        {
            RuleFor(x => x.GoalId)
                .NotEmpty()
                .WithMessage("Goal is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Contribution amount must be greater than zero.");

            RuleFor(x => x.ContributionDate)
                .LessThanOrEqualTo(DateTimeOffset.UtcNow)
                .WithMessage("Contribution date cannot be in the future.");

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Notes cannot exceed 500 characters.");
        }
    }
}
