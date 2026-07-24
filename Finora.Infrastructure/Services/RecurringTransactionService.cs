using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using Finora.Domain.Enums;

namespace Finora.Infrastructure.Services
{
    public class RecurringTransactionService : IRecurringTransactionService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecurringTransactionService(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, IRecurringTransactionRepository recurringTransactionRepository, IUnitOfWork unitOfWork)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _recurringTransactionRepository = recurringTransactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ProcessAsync(RecurringTransaction recurringTransaction, CancellationToken cancellationToken)
        {
            switch (recurringTransaction.TransactionType)
            {
                case TransactionType.Income:
                    await CreateIncomeAsync(recurringTransaction);
                    break;

                case TransactionType.Expense:
                    await CreateExpenseAsync(recurringTransaction);
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported transaction type: {recurringTransaction.TransactionType}");
            }

            var nextDueDate = CalculateNextDueDate(recurringTransaction);

            if (recurringTransaction.EndDate.HasValue &&
                nextDueDate > recurringTransaction.EndDate.Value)
            {
                recurringTransaction.IsActive = false;
            }
            else
            {
                recurringTransaction.NextDueDate = nextDueDate;
            }

            await _recurringTransactionRepository.UpdateAsync(recurringTransaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
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
