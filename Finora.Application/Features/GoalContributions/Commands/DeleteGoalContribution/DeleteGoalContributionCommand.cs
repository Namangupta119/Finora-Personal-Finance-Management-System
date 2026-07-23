using MediatR;

namespace Finora.Application.Features.GoalContributions.Commands.DeleteGoalContribution
{
    public class DeleteGoalContributionCommand : IRequest<DeleteGoalContributionResponse>
    {
        public Guid Id { get; set; }
    }
}
