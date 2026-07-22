using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetExpenseAnalytics
{
    public class GetExpenseAnalyticsQueryHandler : IRequestHandler<GetExpenseAnalyticsQuery, IReadOnlyList<ExpenseAnalyticsDto>>
    {
        private readonly IExpenseRepository _expenseRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetExpenseAnalyticsQueryHandler(IExpenseRepository expenseRepository, ICurrentUserService currentUserService)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<ExpenseAnalyticsDto>> Handle(GetExpenseAnalyticsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            return await _expenseRepository.GetExpenseAnalyticsAsync(userId);
        }
    }
}
