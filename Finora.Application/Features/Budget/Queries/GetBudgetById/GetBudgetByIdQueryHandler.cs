using Finora.Application.Exceptions;
using Finora.Application.Features.Budget.Queries.GetBudgets;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Budget.Queries.GetBudgetById
{
    public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetDto>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetBudgetByIdQueryHandler(IBudgetRepository budgetRepository, ICurrentUserService currentUserService)
        {
            _budgetRepository = budgetRepository;
            _currentUserService = currentUserService;
        }

        public async Task<BudgetDto> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var budget = await _budgetRepository.GetByIdAsync(request.Id, userId);

            if (budget == null)
                throw new NotFoundException("Budget not found.");

            return new BudgetDto
            {
                Id = budget.Id,
                CategoryId = budget.CategoryId,
                CategoryName = budget.Category.Name,
                Amount = budget.Amount,
                Year = budget.Year,
                Month = budget.Month,
            };
        }
    }
}
