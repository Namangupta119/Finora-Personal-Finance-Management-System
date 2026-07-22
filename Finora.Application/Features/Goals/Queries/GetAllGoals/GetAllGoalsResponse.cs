using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Queries.GetAllGoals
{
    public class GetAllGoalsResponse
    {
        public Guid Id { get; set; }
        public string GoalCategory { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal RemainingAmoung { get; set; }
        public decimal PercentageCompleted { get; set; }
        public GoalStatus Status { get; set; }
        public DateTimeOffset? TargetDate { get; set; }
    }
}
