using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Commands.CreateExpense
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Guid>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId, userId);

            if(category is null)
            {
                throw new NotFoundException("Category not found.");
            }

            var expense = new Expense
            {
                Title = request.Title,
                Description = request.Description,
                Amount = request.Amount,
                ExpenseDate = request.ExpenseDate,
                CategoryId = request.CategoryId,
                UserId = userId,
                IsArchived = false
            };

            await _expenseRepository.AddAsync(expense);

            await _expenseRepository.SaveChangesAsync();

            return expense.Id;
        }
    }
}
