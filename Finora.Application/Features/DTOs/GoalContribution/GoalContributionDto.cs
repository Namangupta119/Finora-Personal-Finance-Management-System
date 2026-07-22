using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.DTOs.GoalContribution
{
    public class GoalContributionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ContributionDate { get; set; }
        public string? Notes { get; set; }
    }
}
