using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense
{
    public class MonthlyAmountDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount{ get; set; }
    }
}
