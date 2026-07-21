using Finora.Application.Features.Incomes.Queries.GetAllIncomes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Incomes.Queries.GetIncomeById
{
    public record GetIncomeByIdQuery(Guid Id) : IRequest<IncomeDto>;
}
