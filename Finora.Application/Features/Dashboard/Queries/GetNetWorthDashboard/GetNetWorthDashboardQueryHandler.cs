using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetNetWorthDashboard
{
    public class GetNetWorthDashboardQueryHandler : IRequestHandler<GetNetWorthDashboardQuery, NetWorthDashboardDto>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IInvestmentRepository _investmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetNetWorthDashboardQueryHandler(
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            IInvestmentRepository investmentRepository,
            ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _investmentRepository = investmentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<NetWorthDashboardDto> Handle(GetNetWorthDashboardQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var incomeTask = _incomeRepository.GetTotalIncomeAsync(userId, cancellationToken);
            var expenseTask = _expenseRepository.GetTotalExpenseAsync(userId, cancellationToken);
            var portfolioTask = _investmentRepository.GetCurrentPortfolioValueAsync(userId, cancellationToken);

            var totalIncome = await _incomeRepository.GetTotalIncomeAsync(userId, cancellationToken);

            var totalExpense = await _expenseRepository.GetTotalExpenseAsync(userId, cancellationToken);

            var currentPortfolioValue = await _investmentRepository.GetCurrentPortfolioValueAsync(userId, cancellationToken);

            var cashBalance = totalIncome - totalExpense;

            var netWorth = cashBalance + currentPortfolioValue;

            var savingsRatePercentage = totalIncome == 0 ? 0 : (cashBalance / totalIncome) * 100;

            return new NetWorthDashboardDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                CurrentPortfolioValue = currentPortfolioValue,
                CashBalance = cashBalance,
                NetWorth = netWorth,
                SavingsRate = Math.Round(savingsRatePercentage, 2, MidpointRounding.AwayFromZero)
            };
        }
    }
}
