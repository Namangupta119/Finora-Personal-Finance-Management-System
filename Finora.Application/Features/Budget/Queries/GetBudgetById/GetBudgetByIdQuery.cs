using Finora.Application.Features.Budget.Queries.GetBudgets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Budget.Queries.GetBudgetById
{
    public record GetBudgetByIdQuery(Guid Id) : IRequest<BudgetDto>;
}
