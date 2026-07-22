using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Budget.Queries.GetBudgetVsActual
{
    public class GetBudgetVsActualQuery : IRequest<IReadOnlyList<BudgetVsActualDto>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
