

using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.RecurringTransactions.Commands.UpdateRecurringTransaction
{
    public class UpdateRecurringTransactionCommandHandler : IRequestHandler<UpdateRecurringTransactionCommand, UpdateRecurringTransactionResponse>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateRecurringTransactionCommandHandler(IRecurringTransactionRepository recurringTransactionRepository, ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _recurringTransactionRepository = recurringTransactionRepository;
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateRecurringTransactionResponse> Handle(
            UpdateRecurringTransactionCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Get Existing Transaction
            var recurringTransaction = await _recurringTransactionRepository
                .GetEntityByIdAsync(request.Id, userId);

            if (recurringTransaction == null)
            {
                throw new ApplicationException("Recurring transaction not found.");
            }

            // Validate Category
            var categoryExists = await _categoryRepository
                .ExistsByIdAsync(request.CategoryId, userId);

            if (!categoryExists)
            {
                throw new ApplicationException("Invalid category.");
            }

            // Update Properties
            recurringTransaction.CategoryId = request.CategoryId;
            recurringTransaction.Title = request.Title;
            recurringTransaction.Description = request.Description;
            recurringTransaction.Amount = request.Amount;
            recurringTransaction.TransactionType = request.TransactionType;
            recurringTransaction.Frequency = request.Frequency;
            recurringTransaction.StartDate = request.StartDate;
            recurringTransaction.EndDate = request.EndDate;

            // Audit
            recurringTransaction.UpdatedOn = DateTimeOffset.UtcNow;

            await _recurringTransactionRepository.UpdateAsync(recurringTransaction);

            return new UpdateRecurringTransactionResponse
            {
                Id = recurringTransaction.Id,
                Message = "Recurring transaction updated successfully."
            };
        }
    }

}
