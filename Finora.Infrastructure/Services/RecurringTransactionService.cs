using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using Finora.Domain.Enums;
using Microsoft.Extensions.Logging;


namespace Finora.Infrastructure.Services
{
    public class RecurringTransactionService : IRecurringTransactionService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RecurringTransactionService> _logger;
        public RecurringTransactionService(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, IRecurringTransactionRepository recurringTransactionRepository, IUnitOfWork unitOfWork, ILogger<RecurringTransactionService> logger)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _recurringTransactionRepository = recurringTransactionRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task ProcessAsync(RecurringTransaction recurringTransaction, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                 switch (recurringTransaction.TransactionType)
                 {
                    case TransactionType.Income:

                        var incomeExists =
                            await _incomeRepository.ExistsRecurringIncomeAsync(
                                recurringTransaction.Id,
                                recurringTransaction.NextDueDate,
                                cancellationToken);

                        if (!incomeExists)
                        {
                            await CreateIncomeAsync(recurringTransaction);
                        }
                        else{
                            _logger.LogInformation("Recurring income already exists for transaction {TransactionId}. Skipping income creation.", recurringTransaction.Id);
                        }
                        break;

                    case TransactionType.Expense:

                        var expenseExists =
                            await _expenseRepository.ExistsRecurringExpenseAsync(
                                recurringTransaction.Id,
                                recurringTransaction.NextDueDate,
                                cancellationToken);

                        if (!expenseExists)
                        {
                            await CreateExpenseAsync(recurringTransaction);
                        }
                        else
                        {
                            _logger.LogInformation("Recurring expense already exists for transaction {TransactionId}. Skipping expense creation.", recurringTransaction.Id);
                        }
                        break;

                    default:
                        throw new InvalidOperationException(
                            $"Unsupported transaction type: {recurringTransaction.TransactionType}");
                 }

                var nextDueDate = CalculateNextDueDate(recurringTransaction);

                if (recurringTransaction.EndDate.HasValue && nextDueDate > recurringTransaction.EndDate.Value)
                {
                    recurringTransaction.IsActive = false;
                }
                else
                {
                    recurringTransaction.NextDueDate = nextDueDate;
                }

                await _recurringTransactionRepository.UpdateAsync(recurringTransaction);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        private async Task CreateIncomeAsync(RecurringTransaction recurringTransaction)
        {
            var income = new Income
            {
                Title = recurringTransaction.Title,
                Description = recurringTransaction.Description,
                Amount = recurringTransaction.Amount,
                IncomeDate = recurringTransaction.NextDueDate,
                UserId = recurringTransaction.UserId,

                RecurringTransactionId = recurringTransaction.Id,
                RecurringOccurrenceDate = recurringTransaction.NextDueDate,

                IsArchived = false
            };

            await _incomeRepository.AddAsync(income);
        }

        private async Task CreateExpenseAsync(RecurringTransaction recurringTransaction)
        {
            var expense = new Expense
            {
                Title = recurringTransaction.Title,
                Description = recurringTransaction.Description,
                Amount = recurringTransaction.Amount,
                ExpenseDate = recurringTransaction.NextDueDate,
                CategoryId = recurringTransaction.CategoryId,
                UserId = recurringTransaction.UserId,

                RecurringTransactionId = recurringTransaction.Id,
                RecurringOccurrenceDate = recurringTransaction.NextDueDate,
                
                IsArchived = false
            };

            await _expenseRepository.AddAsync(expense);
        }

        private DateTimeOffset CalculateNextDueDate(RecurringTransaction recurring)
        {
            return recurring.Frequency switch
            {
                RecurrenceFrequency.Daily => recurring.NextDueDate.AddDays(1),

                RecurrenceFrequency.Weekly => recurring.NextDueDate.AddDays(7),

                RecurrenceFrequency.Monthly => recurring.NextDueDate.AddMonths(1),

                RecurrenceFrequency.Yearly => recurring.NextDueDate.AddYears(1),

                _ => throw new InvalidOperationException("Invalid recurrence frequency.")
            };
        }
    }
}
