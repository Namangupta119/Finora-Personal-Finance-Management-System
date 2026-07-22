using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Budget.Queries.GetBudgets
{
    public class BudgetDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId {  get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
