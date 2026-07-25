using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary
{
    public class GetDateRangeFinancialSummaryQueryHandler : IRequestHandler<GetDateRangeFinancialSummaryQuery, DateRangeFinancialSummaryDto>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetDateRangeFinancialSummaryQueryHandler(
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<DateRangeFinancialSummaryDto> Handle(GetDateRangeFinancialSummaryQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var totalIncome = await _incomeRepository.GetTotalIncomeByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            var totalExpense = await _expenseRepository.GetTotalExpenseByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            var incomeTransactionCount = await _incomeRepository.GetIncomeTransactionCountByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            var expenseTransactionCount = await _expenseRepository.GetExpenseTransactionCountByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            return new DateRangeFinancialSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetSavings = totalIncome - totalExpense,
                TransactionCount = incomeTransactionCount + expenseTransactionCount
            };
        }
    }
}
