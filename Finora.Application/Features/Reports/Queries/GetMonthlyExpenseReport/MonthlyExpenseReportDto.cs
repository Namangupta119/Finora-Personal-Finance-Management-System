using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport
{
    public class MonthlyExpenseReportDto
    {
        public int Month { get; set; }

        public string MonthName { get; set; } = string.Empty;

        public decimal TotalExpense { get; set; }
    }
}
