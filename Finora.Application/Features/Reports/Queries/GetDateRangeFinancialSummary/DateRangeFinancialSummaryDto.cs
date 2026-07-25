using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary
{
    public class DateRangeFinancialSummaryDto
    {
        public decimal TotalIncome { get; set; }

        public decimal TotalExpense { get; set; }

        public decimal NetSavings { get; set; }

        public int TransactionCount { get; set; }
    }
}
