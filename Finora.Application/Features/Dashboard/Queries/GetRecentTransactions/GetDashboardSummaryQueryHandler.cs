using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Finora.Application.Features.Dashboard.Queries.GetRecentTransactions
{
    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetDashboardSummaryQueryHandler(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<DashboardSummaryDto> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var totalIncome = await _incomeRepository.GetTotalIncomeAsync(userId);

            var totalExpense = await _expenseRepository.GetTotalExpenseAsync(userId);

            var currentBalance = totalIncome - totalExpense;

            return new DashboardSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                CurrentBalance = currentBalance
            };

        }
    }
}
