using Finora.Application.Features.RecurringTransactions.DTOs;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.RecurringTransactions.Queries.GetRecurringTransactionById
{
    public class GetRecurringTransactionByIdQueryHandler : IRequestHandler<GetRecurringTransactionByIdQuery, RecurringTransactionDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;

        public GetRecurringTransactionByIdQueryHandler(ICurrentUserService currentUserService, IRecurringTransactionRepository recurringTransactionRepository)
        {
            _currentUserService = currentUserService;
            _recurringTransactionRepository = recurringTransactionRepository;
        }

        public async Task<RecurringTransactionDto> Handle(GetRecurringTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var recurringTransaction = await _recurringTransactionRepository.GetByIdAsync(request.Id, userId);

            if (recurringTransaction == null)
            {
                throw new ApplicationException("Recurring transaction not found.");
            }

            return recurringTransaction;
        }
    }
}
