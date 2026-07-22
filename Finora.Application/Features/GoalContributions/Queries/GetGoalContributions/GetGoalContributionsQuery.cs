using Finora.Application.Features.DTOs.GoalContribution;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.GoalContributions.Queries.GetGoalContributions
{
    public class GetGoalContributionsQuery : IRequest<IReadOnlyList<GoalContributionDto>>
    {
        public Guid GoalId { get; set; }
    }
}
