using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetNetWorthDashboard
{
    public class NetWorthDashboardDto
    {
        public decimal TotalIncome { get; set; }

        public decimal TotalExpense { get; set; }

        public decimal CurrentPortfolioValue { get; set; }

        public decimal CashBalance { get; set; }

        public decimal NetWorth { get; set; }

        public decimal SavingsRate { get; set; }
    }
}
