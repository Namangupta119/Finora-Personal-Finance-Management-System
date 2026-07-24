using Finora.Application.Features.RecurringTransactions.DTOs;
using Finora.Domain.Entities;

namespace Finora.Application.Interfaces.Repositories;

public interface IRecurringTransactionRepository
{
    Task AddAsync(RecurringTransaction recurringTransaction);

    Task UpdateAsync(RecurringTransaction recurringTransaction);

    Task<RecurringTransaction?> GetEntityByIdAsync(Guid id, Guid userId);

    Task<List<RecurringTransactionDto>> GetAllAsync(Guid userId);

    Task<RecurringTransactionDto?> GetByIdAsync(Guid id, Guid userId);
    Task<List<RecurringTransaction>> GetDueRecurringTransactionsAsync(DateTimeOffset currentDate);
}