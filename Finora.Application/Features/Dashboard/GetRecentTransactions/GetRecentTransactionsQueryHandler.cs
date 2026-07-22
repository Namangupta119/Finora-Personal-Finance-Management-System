using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Dashboard.GetRecentTransactions
{
    public class GetRecentTransactionsQueryHandler : IRequestHandler<GetRecentTransactionsQuery, IReadOnlyList<RecentTransactionDto>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetRecentTransactionsQueryHandler(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        private const int RecentTransactionCount = 5;
        public async Task<IReadOnlyList<RecentTransactionDto>> Handle(GetRecentTransactionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var recentIncomes = await _incomeRepository.GetRecentIncomesAsync(userId, RecentTransactionCount);

            var recentExpenses = await _expenseRepository.GetRecentExpensesAsync(userId, RecentTransactionCount);

            var incomeTransactions = recentIncomes.Select(x => new RecentTransactionDto
            {
                Id = x.Id,
                Title = x.Title,
                Amount = x.Amount,
                Date = x.IncomeDate,
                Type = "Income"
            });

            var expenseTransactions = recentExpenses.Select(x => new RecentTransactionDto
            {
                Id = x.Id,
                Title = x.Title,
                Amount = x.Amount,
                Date = x.ExpenseDate,
                Type = "Expense"
            });

            var transactions = incomeTransactions.Concat(expenseTransactions).OrderByDescending(x => x.Date).Take(RecentTransactionCount).ToList();

            return transactions;
        }
    }
}
