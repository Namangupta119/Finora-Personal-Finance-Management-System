using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Queries.GetInvestments
{
    public class PagedInvestmentResponse
    {
        public List<InvestmentDto> Investments { get; set; } = [];

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
