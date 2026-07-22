using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System.Globalization;

namespace Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense
{
    public class GetMonthlyIncomeExpenseQueryHandler : IRequestHandler<GetMonthlyIncomeExpenseQuery, IReadOnlyList<MonthlyIncomeExpenseDto>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMonthlyIncomeExpenseQueryHandler(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<MonthlyIncomeExpenseDto>> Handle(GetMonthlyIncomeExpenseQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var incomes = await _incomeRepository.GetMonthlyIncomeAsync(userId);

            var expenses = await _expenseRepository.GetMonthlyExpenseAsync(userId);

            //await Task.WhenAll(incomeTask, expenseTask);

            //var incomes = incomeTask;

            //var expenses = expenseTask;

            var result = new Dictionary<(int Year, int Month), MonthlyIncomeExpenseDto>();

            foreach(var income in incomes)
            {
                var monthKey = (income.Year, income.Month);

                result[monthKey] = new MonthlyIncomeExpenseDto
                {
                    Year = income.Year,
                    Month = income.Month,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(income.Month),
                    Income = income.TotalAmount,
                    Expense = 0
                };
            }

            foreach(var expense in expenses)
            {
                var monthKey = (expense.Year, expense.Month);

                if(result.TryGetValue(monthKey, out var monthlyData))
                {
                    monthlyData.Expense = expense.TotalAmount;
                }
                else
                {
                    result[monthKey] = new MonthlyIncomeExpenseDto
                    {
                        Year = expense.Year,
                        Month = expense.Month,
                        MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(expense.Month),
                        Income = 0,
                        Expense = expense.TotalAmount
                    };
                }
            }

            return result.Values.OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
        }
    }
}
