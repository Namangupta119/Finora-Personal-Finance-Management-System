using Finora.Application.Exceptions;
using Finora.Application.Features.DTOs.GoalContribution;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.GoalContributions.Queries.GetGoalContributions
{
    public class GetGoalContributionsQueryHandler : IRequestHandler<GetGoalContributionsQuery, IReadOnlyList<GoalContributionDto>>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IGoalContributionRepository _goalContributionRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetGoalContributionsQueryHandler(IGoalRepository goalRepository,IGoalContributionRepository goalContributionRepository, ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _goalContributionRepository = goalContributionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<GoalContributionDto>> Handle(GetGoalContributionsQuery request, CancellationToken cancellationToken)
        {
            var goal = await _goalRepository.GetEntityByIdAsync(request.GoalId, _currentUserService.UserId);

            if (goal == null)
            {
                throw new NotFoundException("Goal not found.");
            }

            return await _goalContributionRepository.GetByGoalIdAsync(request.GoalId, _currentUserService.UserId);
        }
    }
}
