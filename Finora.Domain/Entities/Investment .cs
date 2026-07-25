using Finora.Domain.Common;
using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities
{
    public class Investment : BaseEntity
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public InvestmentType Type { get; set; }

        public decimal Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public DateTimeOffset PurchaseDate { get; set; }

        public string? Broker { get; set; }

        public string? Notes { get; set; }
        public string? Symbol { get; set; }

        public bool IsArchived { get; set; }
    }
}
