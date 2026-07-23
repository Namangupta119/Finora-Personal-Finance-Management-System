using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Commands.CreateRecurringTransaction
{
    public class CreateRecurringTransactionResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
