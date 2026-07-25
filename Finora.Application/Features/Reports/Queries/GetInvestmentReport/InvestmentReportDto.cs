using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetInvestmentReport
{
    public class InvestmentReportDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public InvestmentType Type { get; set; }

        public decimal Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal TotalInvestedAmount { get; set; }

        public decimal CurrentValue { get; set; }

        public decimal ProfitLoss { get; set; }

        public decimal ProfitLossPercentage { get; set; }

        public DateTimeOffset PurchaseDate { get; set; }

        public string? Broker { get; set; }

        public string? Symbol { get; set; }
    }
}
