using Finora.Application.Common.Attributes;
using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetInvestmentReport
{
    public class InvestmentReportDto
    {
        [IgnoreColumn]
        public Guid Id { get; set; }

        [Display(Name = "Investment Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Investment Type")]
        public InvestmentType Type { get; set; }

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Current Price")]
        public decimal CurrentPrice { get; set; }

        [Display(Name = "Total Invested")]
        public decimal TotalInvestedAmount { get; set; }

        [Display(Name = "Current Value")]
        public decimal CurrentValue { get; set; }

        [Display(Name = "Profit / Loss")]
        public decimal ProfitLoss { get; set; }

        [Display(Name = "Profit / Loss (%)")]
        public decimal ProfitLossPercentage { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTimeOffset PurchaseDate { get; set; }

        [Display(Name = "Broker")]
        public string? Broker { get; set; }

        [Display(Name = "Symbol")]
        public string? Symbol { get; set; }
    }
}
