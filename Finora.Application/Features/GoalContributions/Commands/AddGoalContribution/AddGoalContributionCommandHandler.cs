using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using Finora.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.GoalContributions.Commands.AddGoalContribution
{
    public class AddGoalContributionCommandHandler : IRequestHandler<AddGoalContributionCommand, AddGoalContributionResponse>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IGoalContributionRepository _goalContributionRepository;
        private readonly ICurrentUserService _currentUserService;

        public AddGoalContributionCommandHandler(IGoalRepository goalRepository ,IGoalContributionRepository goalContributionRepository, ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _goalContributionRepository = goalContributionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<AddGoalContributionResponse> Handle(AddGoalContributionCommand request, CancellationToken cancellationToken)
        {
            var goal = await _goalRepository.GetEntityByIdAsync(
                request.GoalId,
                _currentUserService.UserId);

            if (goal == null)
            {
                throw new NotFoundException("Goal not found.");
            }

            var contribution = new GoalContribution
            {
                GoalId = request.GoalId,
                Amount = request.Amount,
                ContributionDate = request.ContributionDate,
                Notes = request.Notes?.Trim(),
                CreatedOn = DateTimeOffset.UtcNow,
                IsArchived = false
            };

            await _goalContributionRepository.AddAsync(contribution);

            var totalContribution = await _goalContributionRepository
                .GetTotalContributionAsync(request.GoalId);

            if (totalContribution >= goal.TargetAmount &&
                goal.Status != GoalStatus.Completed)
            {
                goal.Status = GoalStatus.Completed;
                goal.UpdatedOn = DateTimeOffset.UtcNow;

                await _goalRepository.UpdateAsync(goal);
            }

            return new AddGoalContributionResponse
            {
                Message = "Contribution added successfully."
            };
        }
    }
}
