using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetRecentTransactions
{
    public class DashboardSummaryDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense{ get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
