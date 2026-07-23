using Finora.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Commands.CreateRecurringTransaction
{
    public class CreateRecurringTransactionCommand : IRequest<CreateRecurringTransactionResponse>
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public RecurrenceFrequency Frequency { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }
    }
}
