using MediatR;

namespace Finora.Application.Features.Goals.Commands.DeleteGoal
{
    public class DeleteGoalCommand : IRequest<DeleteGoalResponse>
    {
        public Guid Id { get; set; }
    }
}
