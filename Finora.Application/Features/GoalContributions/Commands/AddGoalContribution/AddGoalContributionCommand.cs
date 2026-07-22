using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.GoalContributions.Commands.AddGoalContribution
{
    public class AddGoalContributionCommand : IRequest<AddGoalContributionResponse>
    {
        public Guid GoalId { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset ContributionDate { get; set; }

        public string? Notes { get; set; }
    }
}