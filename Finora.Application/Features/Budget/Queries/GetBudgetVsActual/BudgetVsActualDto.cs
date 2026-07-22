using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Budget.Queries.GetBudgetVsActual
{
    public class BudgetVsActualDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal BudgetAmount { get; set; }
        public decimal ActualExpense {  get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal PercentageUsed { get; set; }
        public BudgetStatus Status { get; set; }
        public BudgetAlertLevel AlertLevel { get; set; }
    }
}
