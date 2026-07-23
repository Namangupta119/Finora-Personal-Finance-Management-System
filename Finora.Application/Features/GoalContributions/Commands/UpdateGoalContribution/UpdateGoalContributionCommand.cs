using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.GoalContributions.Commands.UpdateGoalContribution
{
    public class UpdateGoalContributionCommand : IRequest<UpdateGoalContributionResponse>
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ContributionDate { get; set; }
        public string? Notes { get; set; }
    }
}
