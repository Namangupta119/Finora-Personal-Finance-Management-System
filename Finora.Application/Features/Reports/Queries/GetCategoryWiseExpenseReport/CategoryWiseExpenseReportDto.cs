using Finora.Application.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport
{
    public class CategoryWiseExpenseReportDto
    {
        [IgnoreColumn]
        public Guid CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; } = string.Empty;

        [Display(Name = "Total Expense")]
        public decimal TotalExpense { get; set; }
    }
}
