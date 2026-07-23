using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Commands.DeleteRecurringTransaction
{
    public class DeleteRecurringTransactionResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
