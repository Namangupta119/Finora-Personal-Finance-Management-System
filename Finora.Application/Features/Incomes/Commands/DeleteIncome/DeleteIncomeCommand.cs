using MediatR;

namespace Finora.Application.Features.Incomes.Commands.DeleteIncome
{
    public record DeleteIncomeCommand(Guid Id) : IRequest;
}
