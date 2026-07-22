using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense
{
    public record GetMonthlyIncomeExpenseQuery : IRequest<IReadOnlyList<MonthlyIncomeExpenseDto>>;
}
