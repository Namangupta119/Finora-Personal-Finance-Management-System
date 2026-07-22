using Finora.Domain.Entities.Identity;
using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities
{
    public class Goal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GoalCategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal TargetAmount { get; set; }
        public DateTimeOffset? TargetDate { get; set; }
        public GoalStatus Status { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public bool IsArchived { get; set; }
        public User User { get; set; } = default!;
        public GoalCategory GoalCategory { get; set; } = default!;
        public ICollection<GoalContribution> GoalContributions { get; set; } = new List<GoalContribution>();
    }
}
