using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using Finora.Domain.Enums;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBudgetRepository _budgetRepository;
        private readonly INotificationRepository _notificationRepository;

        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IBudgetRepository budgetRepository, INotificationRepository notificationRepository)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _budgetRepository = budgetRepository;
            _notificationRepository = notificationRepository;
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

            var budget = await _budgetRepository.GetBudgetByCategoryAndMonthAsync(
                userId,
                request.CategoryId,
                request.ExpenseDate.Year,
                request.ExpenseDate.Month);


            if (budget is not null)
            {
                var totalExpense = await _expenseRepository.GetTotalExpenseAsync(
                    userId,
                    request.CategoryId,
                    request.ExpenseDate.Year,
                    request.ExpenseDate.Month);

                // Current expense abhi database me save nahi hui hai.
                totalExpense += request.Amount;

                if (budget.Amount > 0 && totalExpense > budget.Amount)
                {
                    var notification = new Notification
                    {
                        UserId = userId,
                        Title = "Budget Exceeded",
                        Message = $"You have exceeded your {category.Name} budget. Budget: ₹{budget.Amount:N2}, Spent: ₹{totalExpense:N2}.",
                        IsRead = false,
                        IsArchived = false,
                        ActionUrl = "/budgets"
                    };

                    await _notificationRepository.AddAsync(notification);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return expense.Id;

        }
    }
}
