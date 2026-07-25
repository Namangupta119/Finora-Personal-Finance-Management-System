using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Investments.Queries.GetInvestmentDashboard
{
    public class InvestmentDashboardDto
    {
        public decimal TotalInvestedAmount { get; set; }

        public decimal CurrentPortfolioValue { get; set; }

        public decimal ProfitLoss { get; set; }

        public decimal ProfitLossPercentage { get; set; }
    }
}
