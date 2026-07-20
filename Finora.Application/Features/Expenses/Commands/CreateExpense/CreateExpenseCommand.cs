using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Commands.CreateExpense
{
    public record CreateExpenseCommand(string Title, string? Description, decimal Amount, DateTimeOffset ExpenseDate, Guid CategoryId) : IRequest<Guid>;
}
