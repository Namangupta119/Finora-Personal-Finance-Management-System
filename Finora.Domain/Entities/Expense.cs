using Finora.Domain.Common;
using Finora.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set;  }
        public decimal Amount { get; set;  }
        public DateTimeOffset ExpenseDate { get; set;  }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public bool IsArchived { get; set; }
        public Guid? RecurringTransactionId { get; set; }
        public DateTimeOffset? RecurringOccurrenceDate { get; set; }

    }
}
