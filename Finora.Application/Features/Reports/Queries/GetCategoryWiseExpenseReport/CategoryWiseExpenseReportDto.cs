using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport
{
    public class CategoryWiseExpenseReportDto
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public decimal TotalExpense { get; set; }
    }
}
