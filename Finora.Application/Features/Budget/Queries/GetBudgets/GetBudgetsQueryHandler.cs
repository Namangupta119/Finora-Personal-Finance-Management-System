using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Budget.Queries.GetBudgets
{
    public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, IReadOnlyList<BudgetDto>>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetBudgetsQueryHandler(IBudgetRepository budgetRepository, ICurrentUserService currentUserService)
        {
            _budgetRepository = budgetRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            return await _budgetRepository.GetAllBudgetAsync(userId);
        }
    }
}
