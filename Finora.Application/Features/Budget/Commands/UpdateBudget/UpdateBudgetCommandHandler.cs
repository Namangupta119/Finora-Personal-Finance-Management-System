using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Budget.Commands.UpdateBudget
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _budgetRepository = budgetRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var budget = await _budgetRepository.GetByIdAsync(request.Id, userId);

            if (budget == null)
                throw new NotFoundException("Budget not found.");

            var budgetExists = await _budgetRepository.BudgetExistsAsync(userId, request.CategoryId, request.Month, request.Year);

            if (budgetExists)
                throw new InvalidOperationException("A budget already exists for the selected category and month.");

            budget.CategoryId = request.CategoryId;
            budget.Amount = request.Amount;
            budget.Year = request.Year;
            budget.Month = request.Month;

            await _budgetRepository.UpdateAsync(budget);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
