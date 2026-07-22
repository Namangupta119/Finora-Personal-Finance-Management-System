using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finora.Application.Features.Goals.Commands.UpdateGoal
{
    public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, UpdateGoalResponse>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateGoalCommandHandler(IGoalRepository goalRepository, ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateGoalResponse> Handle(
            UpdateGoalCommand request,
            CancellationToken cancellationToken)
        {
            var goal = await _goalRepository.GetEntityByIdAsync(
                request.Id,
                _currentUserService.UserId);

            if (goal == null)
            {
                throw new NotFoundException("Goal not found.");
            }

            var titleExists = await _goalRepository.ExistsByTitleAsync(
                _currentUserService.UserId,
                request.Title,
                request.Id);

            if (titleExists)
            {
                throw new ValidationException("A goal with the same title already exists.");
            }

            goal.GoalCategoryId = request.GoalCategoryId;
            goal.Title = request.Title.Trim();
            goal.Description = request.Description?.Trim();
            goal.TargetAmount = request.TargetAmount;
            goal.TargetDate = request.TargetDate;
            goal.UpdatedOn = DateTimeOffset.UtcNow;

            await _goalRepository.UpdateAsync(goal);

            return new UpdateGoalResponse
            {
                Message = "Goal updated successfully."
            };
        }
    }

}
