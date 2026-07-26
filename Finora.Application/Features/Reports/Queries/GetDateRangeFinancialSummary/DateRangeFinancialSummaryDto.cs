using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary
{
    public class DateRangeFinancialSummaryDto
    {
        [Display(Name = "Total Income")]
        public decimal TotalIncome { get; set; }

        [Display(Name = "Total Expense")]
        public decimal TotalExpense { get; set; }

        [Display(Name = "Net Savings")]
        public decimal NetSavings { get; set; }

        [Display(Name = "Transaction Count")]
        public int TransactionCount { get; set; }
    }
}
