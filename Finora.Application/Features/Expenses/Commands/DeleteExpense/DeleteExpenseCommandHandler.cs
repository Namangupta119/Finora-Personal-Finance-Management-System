using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Expenses.Commands.DeleteExpense
{
    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand> 
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var expense = await _expenseRepository.GetByIdAsync(request.Id, userId);

            if (expense == null)
                throw new NotFoundException("Expense not found.");

            expense.IsArchived = true;

            _expenseRepository.Update(expense);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
