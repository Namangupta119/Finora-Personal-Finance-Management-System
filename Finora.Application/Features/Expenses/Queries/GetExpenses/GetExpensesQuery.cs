using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Queries.GetExpenses
{
    public record GetExpensesQuery : IRequest<IReadOnlyList<ExpensesDto>>;
}
