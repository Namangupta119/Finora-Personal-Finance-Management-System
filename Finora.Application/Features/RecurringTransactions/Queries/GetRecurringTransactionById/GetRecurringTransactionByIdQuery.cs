using Finora.Application.Features.RecurringTransactions.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Queries.GetRecurringTransactionById
{
    public class GetRecurringTransactionByIdQuery : IRequest<RecurringTransactionDto>
    {
        public Guid Id { get; set; }
    }
}
