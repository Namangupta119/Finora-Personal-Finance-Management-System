using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Queries.GetGoalById
{
    public class GetGoalByIdQueryHandler : IRequestHandler<GetGoalByIdQuery, GetGoalByIdResponse>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetGoalByIdQueryHandler(IGoalRepository goalRepository, ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _currentUserService = currentUserService;
        }

        public async Task<GetGoalByIdResponse> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
        {
            var goal = await _goalRepository.GetByIdAsync(request.Id, _currentUserService.UserId);

            if (goal == null)
                throw new NotFoundException("Goal not found.");

            return new GetGoalByIdResponse
            {
                Id = goal.Id,
                GoalCategoryId = goal.GoalCategoryId,
                GoalCategoryName = goal.GoalCategoryName,
                Title = goal.Title,
                Description = goal.Description,
                TargetAmount = goal.TargetAmount,
                CurrentAmount = goal.CurrentAmount,
                RemainingAmount = Math.Max(0, goal.TargetAmount - goal.CurrentAmount),
                PercentageCompleted = goal.TargetAmount == 0 ? 0 : Math.Min(100, Math.Round((goal.CurrentAmount / goal.TargetAmount) * 100, 2)),
                Status = goal.Status,
                TargetDate = goal.TargetDate
            };
        }
    }
}
