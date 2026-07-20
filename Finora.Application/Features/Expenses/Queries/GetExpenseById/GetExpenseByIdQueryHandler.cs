using Finora.Application.Exceptions;
using Finora.Application.Features.Expenses.Queries.GetExpenses;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Queries.GetExpenseById
{
    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ExpensesDto>
    {
        private readonly IExpenseRepository _expenseRepositor;
        private readonly ICurrentUserService _currentUserService;

        public GetExpenseByIdQueryHandler(IExpenseRepository expenseRepository, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _expenseRepositor = expenseRepository;
        }

        public async Task<ExpensesDto> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var expense = await _expenseRepositor.GetByIdAsync(request.Id, userId);

            if (expense is null)
                throw new NotFoundException("Expense not found.");

            return new ExpensesDto
            {
                Id = expense.Id,
                Title = expense.Title,
                Description = expense.Description,
                Amount = expense.Amount,
                ExpenseDate = expense.ExpenseDate,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category.Name
            };
        }
    }
}
