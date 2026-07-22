using MediatR;

namespace Finora.Application.Features.Budget.Queries.GetBudgets
{
    public record GetBudgetsQuery : IRequest<IReadOnlyList<BudgetDto>>;
}
