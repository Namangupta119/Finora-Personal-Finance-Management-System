using Finora.Application.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport
{
    public class MonthlyIncomeReportDto
    {
        [IgnoreColumn]
        public int Month { get; set; }

        [Display(Name = "Month")]
        public string MonthName { get; set; } = string.Empty;

        [Display(Name = "Total Income")]
        public decimal TotalIncome { get; set; }
    }
}
