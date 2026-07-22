using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.DTOs.Goal
{
    public class GoalListDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string GoalCategoryName { get; set; } = string.Empty;

        public decimal TargetAmount { get; set; }

        public decimal CurrentAmount { get; set; }

        public GoalStatus Status { get; set; }

        public DateTimeOffset? TargetDate { get; set; }
    }
}
