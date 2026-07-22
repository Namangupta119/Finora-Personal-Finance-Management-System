using Finora.Domain.Enums;

namespace Finora.Application.Features.DTOs.Goal
{
    public class GoalDetailsDto
    {
        public Guid Id { get; set; }
        public Guid GoalCategoryId { get; set; }
        public string GoalCategoryName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public GoalStatus Status { get; set; }
        public DateTimeOffset? TargetDate { get; set; }
    }
}
