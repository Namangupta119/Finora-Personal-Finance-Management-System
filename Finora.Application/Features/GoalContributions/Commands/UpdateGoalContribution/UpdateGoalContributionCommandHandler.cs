using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Enums;
using MediatR;

namespace Finora.Application.Features.GoalContributions.Commands.UpdateGoalContribution
{
    public class UpdateGoalContributionCommandHandler : IRequestHandler<UpdateGoalContributionCommand, UpdateGoalContributionResponse>
    {
        private readonly IGoalContributionRepository _goalContributionRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly ICurrentUserService _currentUserService;
        public UpdateGoalContributionCommandHandler(IGoalContributionRepository goalContributionRepository, IGoalRepository goalRepository, ICurrentUserService currentUserService)
        {
            _goalContributionRepository = goalContributionRepository;
            _goalRepository = goalRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateGoalContributionResponse> Handle(UpdateGoalContributionCommand request, CancellationToken cancellationToken)
        {
            var contribution = await _goalContributionRepository.GetEntityByIdAsync(request.Id, _currentUserService.UserId);

            if (contribution == null)
            {
                throw new NotFoundException("Contribution not found.");
            }

            contribution.Amount = request.Amount;
            contribution.ContributionDate = request.ContributionDate;
            contribution.Notes = request.Notes?.Trim();
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

            return new UpdateGoalContributionResponse
            {
                Message = "Contribution updated successfully."
            };
        }
    }
}
