using Finora.Application.Features.RecurringTransactions.DTOs;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Queries.GetAllRecurringTransactions
{
    public class GetAllRecurringTransactionsQueryHandler : IRequestHandler<GetAllRecurringTransactionsQuery, List<RecurringTransactionDto>>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllRecurringTransactionsQueryHandler(IRecurringTransactionRepository recurringTransactionRepository, ICurrentUserService currentUserService)
        {
            _recurringTransactionRepository = recurringTransactionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<RecurringTransactionDto>> Handle(GetAllRecurringTransactionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            return await _recurringTransactionRepository.GetAllAsync(userId);
        }
    }
}
