using Finora.Application.Features.Expenses.Queries.GetExpenses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Commands.UpdateExpense
{
    public record UpdateExpenseCommand(Guid Id, string Title, string? Description, decimal Amount, DateTimeOffset ExpenseDate, Guid CategoryId) : IRequest;
}
