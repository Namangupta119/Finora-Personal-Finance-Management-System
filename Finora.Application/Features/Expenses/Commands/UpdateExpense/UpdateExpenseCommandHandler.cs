using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Expenses.Commands.UpdateExpense
{
    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateExpenseCommandHandler(IExpenseRepository expenseRepository,ICurrentUserService currentUserService, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var expense = await _expenseRepository.GetByIdAsync(request.Id, userId);

            if (expense is null)
                throw new NotFoundException("Expense not found.");

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId, userId);

            if (category is null)
                throw new NotFoundException("Category not found.");

            expense.Title = request.Title;
            expense.Description = request.Description;
            expense.Amount = request.Amount;
            expense.ExpenseDate = request.ExpenseDate;
            expense.CategoryId = request.CategoryId;

            _expenseRepository.Update(expense);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
