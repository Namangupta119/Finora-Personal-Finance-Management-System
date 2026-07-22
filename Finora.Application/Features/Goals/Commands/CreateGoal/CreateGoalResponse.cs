using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Commands.CreateGoal
{
    public class CreateGoalResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
