using Finora.Application.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport
{
    public class MonthlyExpenseReportDto
    {
        [IgnoreColumn]
        public int Month { get; set; }

        [Display(Name = "Month")]
        public string MonthName { get; set; } = string.Empty;

        [Display(Name = "Total Expense")]
        public decimal TotalExpense { get; set; }
    }
}
