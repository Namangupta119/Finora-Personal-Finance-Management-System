using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.RecurringTransactions.Commands.CreateRecurringTransaction
{
    public class CreateRecurringTransactionCommandHandler : IRequestHandler<CreateRecurringTransactionCommand, CreateRecurringTransactionResponse>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateRecurringTransactionCommandHandler(IRecurringTransactionRepository recurringTransactionRepository,ICategoryRepository categoryRepository,ICurrentUserService currentUserService)
        {
            _recurringTransactionRepository = recurringTransactionRepository;
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CreateRecurringTransactionResponse> Handle(CreateRecurringTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Check Category Exists
            var categoryExists = await _categoryRepository.ExistsByIdAsync(request.CategoryId, userId);

            if (!categoryExists)
            {
                throw new ApplicationException("Invalid category.");
            }

            var recurringTransaction = new RecurringTransaction
            {
                UserId = userId,
                CategoryId = request.CategoryId,
                Title = request.Title,
                Description = request.Description,
                Amount = request.Amount,
                TransactionType = request.TransactionType,
                Frequency = request.Frequency,
                StartDate = request.StartDate,
                EndDate = request.EndDate,

                // First occurrence
                NextDueDate = request.StartDate,

                IsActive = true,

                CreatedOn = DateTimeOffset.UtcNow,
                UpdatedOn = null
            };

            await _recurringTransactionRepository.AddAsync(recurringTransaction);

            return new CreateRecurringTransactionResponse
            {
                Id = recurringTransaction.Id,
                Message = "Recurring transaction created successfully."
            };
        }
    }
}
