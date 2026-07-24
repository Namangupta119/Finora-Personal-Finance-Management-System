using Finora.Application.Features.RecurringTransactions.DTOs;
using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories;

public class RecurringTransactionRepository : IRecurringTransactionRepository
{
    private readonly ApplicationDbContext _context;

    public RecurringTransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RecurringTransaction recurringTransaction)
    {
        await _context.RecurringTransactions.AddAsync(recurringTransaction);

        await _context.SaveChangesAsync();
    }

    public async Task<List<RecurringTransactionDto>> GetAllAsync(Guid userId)
    {
        return await _context.RecurringTransactions
        .AsNoTracking()
        .Where(x => x.UserId == userId)
        .OrderBy(x => x.NextDueDate)
        .Select(x => new RecurringTransactionDto
        {
            // Mapping
            Id = x.Id,
            CategoryId = x.CategoryId,
            CategoryName = x.Category.Name,
            Title = x.Title,
            Description = x.Description,
            Amount = x.Amount,
            TransactionType = x.TransactionType,
            Frequency = x.Frequency,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            NextDueDate = x.NextDueDate,
            IsActive = x.IsActive
        })
        .ToListAsync();
    }

    public async Task<RecurringTransactionDto?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.RecurringTransactions
        .AsNoTracking()
        .Where(x => x.Id == id && x.UserId == userId)
        .Select(x => new RecurringTransactionDto
        {
            // Mapping
            Id = x.Id,
            CategoryId = x.CategoryId,
            CategoryName = x.Category.Name,
            Title = x.Title,
            Description = x.Description,
            Amount = x.Amount,
            TransactionType = x.TransactionType,
            Frequency = x.Frequency,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            NextDueDate = x.NextDueDate,
            IsActive = x.IsActive
        })
        .FirstOrDefaultAsync();
    }

    public async Task<RecurringTransaction?> GetEntityByIdAsync(Guid id, Guid userId)
    {
        return await _context.RecurringTransactions
        .FirstOrDefaultAsync(x =>
            x.Id == id &&
            x.UserId == userId);
    }

    public async Task UpdateAsync(RecurringTransaction recurringTransaction)
    {
        _context.RecurringTransactions.Update(recurringTransaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RecurringTransaction>> GetDueRecurringTransactionsAsync(DateTimeOffset currentDate)
    {
        return await _context.RecurringTransactions.Where(x => x.IsActive && x.NextDueDate <= currentDate).ToListAsync();
    }
}