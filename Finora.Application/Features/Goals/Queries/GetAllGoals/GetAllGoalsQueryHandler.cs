using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Queries.GetAllGoals
{
    public class GetAllGoalsQueryHandler :  IRequestHandler<GetAllGoalsQuery, List<GetAllGoalsResponse>>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllGoalsQueryHandler(
            IGoalRepository goalRepository,
            ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<GetAllGoalsResponse>> Handle(GetAllGoalsQuery request, CancellationToken cancellationToken)
        {
            var goals = await _goalRepository.GetAllByUserIdAsync(_currentUserService.UserId);

            var response = goals.Select(goal => new GetAllGoalsResponse
            {
                Id = goal.Id,
                Title = goal.Title,
                Description = goal.Description,
                GoalCategory = goal.GoalCategoryName,
                TargetAmount = goal.TargetAmount,
                CurrentAmount = goal.CurrentAmount,
                RemainingAmoung = Math.Max(0, goal.TargetAmount - goal.CurrentAmount),
                PercentageCompleted = goal.TargetAmount == 0 ? 0 : Math.Round((goal.CurrentAmount / goal.TargetAmount) * 100, 2),
                Status = goal.Status,
                TargetDate = goal.TargetDate
            }).ToList();

            return response;
        }
    }
}
