using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Queries.GetExpenses
{
    public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IReadOnlyList<ExpensesDto>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetExpensesQueryHandler(IExpenseRepository expenseRepository, ICurrentUserService currentUserService)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<ExpensesDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var expenses = await _expenseRepository.GetExpensesAsync(userId);

            return expenses.Select(x => new ExpensesDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Amount = x.Amount,
                ExpenseDate = x.ExpenseDate,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name
            }).ToList();
        }
    }
}
