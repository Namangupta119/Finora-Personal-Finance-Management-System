using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetExpenseAnalytics
{
    public class ExpenseAnalyticsDto
    {
        public string Category { get; set; } = default!;
        public decimal TotalAmount { get; set; }
    }
}
