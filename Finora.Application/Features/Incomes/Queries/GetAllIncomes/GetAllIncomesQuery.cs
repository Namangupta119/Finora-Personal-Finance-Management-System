using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Incomes.Queries.GetAllIncomes
{
    public record GetAllIncomesQuery : IRequest<IReadOnlyList<IncomeDto>>;
}
