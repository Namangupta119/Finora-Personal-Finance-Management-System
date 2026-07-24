using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Services
{
    public interface IRecurringTransactionService
    {
        Task ProcessAsync(RecurringTransaction recurringTransaction, CancellationToken cancellationToken);
    }
}
