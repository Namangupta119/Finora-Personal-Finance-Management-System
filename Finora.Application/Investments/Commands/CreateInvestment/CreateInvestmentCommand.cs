using Finora.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Commands.CreateInvestment
{
    public class CreateInvestmentCommand : IRequest<Guid>
    {
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
