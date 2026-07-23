using Finora.Application.Features.RecurringTransactions.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Queries.GetAllRecurringTransactions
{
    public class GetAllRecurringTransactionsQuery : IRequest<List<RecurringTransactionDto>>
    {
    }
}
