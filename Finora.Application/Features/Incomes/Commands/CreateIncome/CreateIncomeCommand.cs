using MediatR;

namespace Finora.Application.Features.Incomes.Commands.CreateIncome
{
    public record CreateIncomeCommand(string Title, string? Description, decimal Amount, DateTimeOffset IncomeDate) : IRequest<Guid>;
}
