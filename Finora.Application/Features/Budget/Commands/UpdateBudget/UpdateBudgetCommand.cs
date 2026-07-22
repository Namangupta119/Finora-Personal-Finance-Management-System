using MediatR;

namespace Finora.Application.Features.Budget.Commands.UpdateBudget
{
    public record UpdateBudgetCommand(Guid Id, Guid CategoryId, decimal Amount, int Year, int Month) : IRequest;
}
