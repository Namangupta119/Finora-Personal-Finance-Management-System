using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Finora.Application.Features.Budget.Commands.CreateBudget
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, Guid>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBudgetCommandHandler(IBudgetRepository budgetRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _budgetRepository = budgetRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var budgetExists = await _budgetRepository.BudgetExistsAsync(userId, request.CategoryId, request.Year, request.Month);

            if(budgetExists)
                throw new ValidationException("Budget already exists for this category and month.");

            var budget = new Finora.Domain.Entities.Budget
            {
                UserId = userId,
                CategoryId = request.CategoryId,
                Amount = request.Amount,
                Year = request.Year,
                Month = request.Month,
            };

            await _budgetRepository.AddAsync(budget);

            await _unitOfWork.SaveChangesAsync();

            return budget.Id;
        }
    }
}
