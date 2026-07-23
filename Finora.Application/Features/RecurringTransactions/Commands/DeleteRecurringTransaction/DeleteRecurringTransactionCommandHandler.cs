using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Commands.DeleteRecurringTransaction
{
    public class DeleteRecurringTransactionCommandHandler : IRequestHandler<DeleteRecurringTransactionCommand, DeleteRecurringTransactionResponse>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteRecurringTransactionCommandHandler(
            IRecurringTransactionRepository recurringTransactionRepository,
            ICurrentUserService currentUserService)
        {
            _recurringTransactionRepository = recurringTransactionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<DeleteRecurringTransactionResponse> Handle(DeleteRecurringTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var recurringTransaction = await _recurringTransactionRepository.GetEntityByIdAsync(request.Id, userId);

            if (recurringTransaction == null)
            {
                throw new ApplicationException("Recurring transaction not found.");
            }

            recurringTransaction.IsActive = false;
            recurringTransaction.UpdatedOn = DateTimeOffset.UtcNow;

            await _recurringTransactionRepository.UpdateAsync(recurringTransaction);

            return new DeleteRecurringTransactionResponse
            {
                Id = recurringTransaction.Id,
                Message = "Recurring transaction deleted successfully."
            };
        }
    }
}
