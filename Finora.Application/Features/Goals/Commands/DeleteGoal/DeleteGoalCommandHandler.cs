using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Goals.Commands.DeleteGoal
{
    public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, DeleteGoalResponse>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteGoalCommandHandler(IGoalRepository goalRepository, ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _currentUserService = currentUserService;
        }

        public async Task<DeleteGoalResponse> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
        {
            var goal = await _goalRepository.GetEntityByIdAsync(request.Id, _currentUserService.UserId);

            if (goal is null)
                throw new NotFoundException("Goal not found.");

            foreach (var contribution in goal.GoalContributions.Where(x => !x.IsArchived))
            {
                contribution.IsArchived = true;
                contribution.UpdatedOn = DateTimeOffset.UtcNow;
            }

            goal.IsArchived = true;
            goal.UpdatedOn = DateTimeOffset.UtcNow;

            await _goalRepository.UpdateAsync(goal);

            return new DeleteGoalResponse
            {
                Message = "Goal deleted Successfully."
            };
        }
    }
}
