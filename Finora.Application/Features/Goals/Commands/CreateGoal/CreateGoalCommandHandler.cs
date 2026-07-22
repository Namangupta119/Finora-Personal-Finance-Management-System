using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using Finora.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Commands.CreateGoal
{
    public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, CreateGoalResponse>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IGoalCategoryRepository _goalCategoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateGoalCommandHandler(IGoalRepository goalRepository, IGoalCategoryRepository goalCategoryRepository, ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _goalCategoryRepository = goalCategoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CreateGoalResponse> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var goalExists = await _goalRepository.ExistsByTitleAsync(request.GoalCategoryId, request.Title);

            if (goalExists)
                throw new ApplicationException("A goal with the same title already exists.");

            var categoryExists = await _goalCategoryRepository.ExistsAsync(request.GoalCategoryId);

            if (!categoryExists)
                throw new ApplicationException("Invalid goal category.");

            var goal = new Goal
            {
                UserId = userId,
                GoalCategoryId = request.GoalCategoryId,
                Title = request.Title,
                Description = request.Description,
                TargetAmount = request.TargetAmount,
                Status = GoalStatus.Active,
                CreatedOn = DateTimeOffset.UtcNow,
                UpdatedOn = null,
                IsArchived = false
            };
            await _goalRepository.AddAsync(goal);

            return new CreateGoalResponse
            {
                Id = goal.Id,
                Message = "Goal created successfully."
            };
        }
    }
}
