using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Enums;
using MediatR;

namespace Finora.Application.Features.Budget.Queries.GetBudgetVsActual
{
    public class GetBudgetVsActualQueryHandler : IRequestHandler<GetBudgetVsActualQuery, IReadOnlyList<BudgetVsActualDto>>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICurrentUserService _currentUserService;

        private const decimal WarningThreshold = 80m;
        private const decimal CriticalThreshold = 100m;

        public GetBudgetVsActualQueryHandler(IBudgetRepository budgetRepository, ICurrentUserService currentUserService)
        {
            _budgetRepository = budgetRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<BudgetVsActualDto>> Handle(GetBudgetVsActualQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var budgets = await _budgetRepository.GetBudgetVsActualAsync(userId, request.Year, request.Month);

            foreach(var budget in budgets )
            {
                budget.RemainingAmount = budget.BudgetAmount - budget.ActualExpense;

                var percentageUsed = budget.BudgetAmount == 0 ? 0 : (budget.ActualExpense / budget.BudgetAmount) * 100;

                budget.PercentageUsed = percentageUsed;

                //budget status
                if (budget.ActualExpense < budget.BudgetAmount)
                    budget.Status = BudgetStatus.UnderBudget;
                else if (budget.ActualExpense == budget.BudgetAmount)
                    budget.Status = BudgetStatus.OnBudget;
                else
                    budget.Status = BudgetStatus.OverBudget;

                //Alert level
                if (percentageUsed < WarningThreshold)
                    budget.AlertLevel = BudgetAlertLevel.Safe;
                else if (percentageUsed < CriticalThreshold)
                    budget.AlertLevel = BudgetAlertLevel.Warning;
                else
                    budget.AlertLevel = BudgetAlertLevel.Critical;
            }

            return budgets;
        }
    }
}
