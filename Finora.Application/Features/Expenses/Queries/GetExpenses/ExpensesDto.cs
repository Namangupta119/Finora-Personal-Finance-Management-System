using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Queries.GetExpenses
{
    public class ExpensesDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ExpenseDate{ get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}
