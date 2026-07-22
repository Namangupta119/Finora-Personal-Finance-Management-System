using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.GetRecentTransactions
{
    public class RecentTransactionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Type { get; set; } = default!;
    }
}
