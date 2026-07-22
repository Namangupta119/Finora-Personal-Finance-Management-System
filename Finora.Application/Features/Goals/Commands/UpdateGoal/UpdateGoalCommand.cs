using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Commands.UpdateGoal
{
    public class UpdateGoalCommand : IRequest<UpdateGoalResponse>
    {
        public Guid Id { get; set; }

        public Guid GoalCategoryId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTimeOffset? TargetDate { get; set; }
    }
}
