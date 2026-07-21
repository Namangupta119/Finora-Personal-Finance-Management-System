using MediatR;

namespace Finora.Application.Features.Incomes.Commands.UpdateIncome
{
    public record UpdateIncomeCommand(Guid Id, string Title, string? Description, decimal Amount, DateTimeOffset IncomeDate) : IRequest;
}
