using Finora.Application.Features.Expenses.Queries.GetExpenses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Queries.GetExpenseById
{
    public record GetExpenseByIdQuery(Guid Id) : IRequest<ExpensesDto>;
}
