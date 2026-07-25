using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Queries.GetInvestmentById
{
    public class InvestmentDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Symbol { get; set; }

        public InvestmentType Type { get; set; }

        public decimal Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public DateTimeOffset PurchaseDate { get; set; }

        public string? Broker { get; set; }

        public string? Notes { get; set; }
    }
}
