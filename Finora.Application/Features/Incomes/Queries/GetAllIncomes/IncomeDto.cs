using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Incomes.Queries.GetAllIncomes
{
    public class IncomeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Amount { get; set;  }
        public DateTimeOffset IncomeDate { get; set; }

    }
}
