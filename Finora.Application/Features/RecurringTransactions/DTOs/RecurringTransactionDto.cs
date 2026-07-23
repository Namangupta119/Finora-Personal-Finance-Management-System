using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.DTOs
{
    public class RecurringTransactionDto
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public RecurrenceFrequency Frequency { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public DateTimeOffset NextDueDate { get; set; }

        public bool IsActive { get; set; }
    }
}
