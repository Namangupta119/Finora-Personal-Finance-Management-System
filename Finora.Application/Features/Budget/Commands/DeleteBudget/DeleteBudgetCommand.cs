using MediatR;

namespace Finora.Application.Features.Budget.Commands.DeleteBudget
{
    public record DeleteBudgetCommand(Guid id) : IRequest;
}
