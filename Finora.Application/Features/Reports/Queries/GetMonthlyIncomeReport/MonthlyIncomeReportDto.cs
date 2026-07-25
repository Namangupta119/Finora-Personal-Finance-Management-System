using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport
{
    public class MonthlyIncomeReportDto
    {
        public int Month { get; set; }

        public string MonthName { get; set; } = string.Empty;

        public decimal TotalIncome { get; set; }
    }
}
