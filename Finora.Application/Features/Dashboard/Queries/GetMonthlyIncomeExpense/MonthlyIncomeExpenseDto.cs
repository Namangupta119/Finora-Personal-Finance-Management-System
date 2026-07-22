using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense
{
    public class MonthlyIncomeExpenseDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; } = default!;
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
    }
}
