using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Commands.DeleteRecurringTransaction
{
    public class DeleteRecurringTransactionCommand : IRequest<DeleteRecurringTransactionResponse>
    {
        public Guid Id { get; set; }
    }
}
