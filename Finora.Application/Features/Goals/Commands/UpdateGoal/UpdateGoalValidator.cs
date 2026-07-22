using Finora.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Commands.UpdateGoal
{
    public class UpdateGoalValidator : AbstractValidator<UpdateGoalCommand>
    {
        public UpdateGoalValidator(IGoalCategoryRepository goalCategoryRepository)
        {
            RuleFor(x => x.GoalCategoryId)
                .NotEmpty().WithMessage("Goal category is required.")
                .MustAsync(async (goalCategoryId, cancellationToken) =>
                    await goalCategoryRepository.ExistsAsync(goalCategoryId))
                .WithMessage("Selected goal category does not exist.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.TargetAmount)
                .GreaterThan(0).WithMessage("Target amount must be greater than zero.");

            RuleFor(x => x.TargetDate)
                .GreaterThan(DateTimeOffset.UtcNow)
                .When(x => x.TargetDate.HasValue)
                .WithMessage("Target date must be in the future.");
        }
    }
}
