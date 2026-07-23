using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Enums;
using MediatR;

namespace Finora.Application.Features.GoalContributions.Commands.DeleteGoalContribution
{
    public class DeleteGoalContributionCommandHandler : IRequestHandler<DeleteGoalContributionCommand, DeleteGoalContributionResponse>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IGoalContributionRepository _goalContributionRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteGoalContributionCommandHandler(IGoalRepository goalRepository, IGoalContributionRepository goalContributionRepository, ICurrentUserService currentUserService)
        {
            _goalRepository = goalRepository;
            _goalContributionRepository = goalContributionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<DeleteGoalContributionResponse> Handle(DeleteGoalContributionCommand request, CancellationToken cancellationToken)
        {
            var contribution = await _goalContributionRepository.GetEntityByIdAsync(request.Id, _currentUserService.UserId);

            if (contribution == null)
                throw new NotFoundException("Contribution not found.");

            contribution.IsArchived = true;
            contribution.UpdatedOn = DateTimeOffset.UtcNow;

            await _goalContributionRepository.UpdateAsync(contribution);

            var totalContribution = await _goalContributionRepository.GetTotalContributionAsync(contribution.GoalId);

            var newStatus = totalContribution >= contribution.Goal.TargetAmount ? GoalStatus.Completed : GoalStatus.Active;

            if(contribution.Goal.Status != newStatus)
            {
                contribution.Goal.Status = newStatus;
                contribution.Goal.UpdatedOn = DateTimeOffset.UtcNow;

                await _goalRepository.UpdateAsync(contribution.Goal);
            }

            return new DeleteGoalContributionResponse
            {
                Message = "Contribution deleted successfully."
            };
            
            
        }
    }
}
