using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Domain.Enums;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public Task DeleteAsync(Notification notification)
        {
            notification.IsArchived = true;
            notification.UpdatedOn = DateTimeOffset.UtcNow;
            return Task.CompletedTask;
        }

        public async Task<Notification?> GetByIdAsync(Guid notificationId,Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId && x.UserId == userId && !x.IsArchived, cancellationToken);
        }

        public async Task<List<Notification>> GetByUserIdAsync(Guid userId,int pageNumber,int pageSize,CancellationToken cancellationToken)
        {
            return await _context.Notifications.AsNoTracking().Where(x => x.UserId == userId && !x.IsArchived).OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Notifications.CountAsync(x => x.UserId == userId && !x.IsRead && !x.IsArchived,cancellationToken);
        }

        public async Task MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken)
        {
            await _context.Notifications.Where(x => x.UserId == userId && !x.IsRead && !x.IsArchived).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsRead, true).SetProperty(x => x.ReadOn, DateTimeOffset.UtcNow),cancellationToken);
        }

        public async Task<int> GetTotalCountAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Notifications.CountAsync(x => x.UserId == userId && !x.IsArchived, cancellationToken);
        }
        public async Task<bool> NotificationExisitsAsync(Guid userId,NotificationType type,Guid referenceId,CancellationToken cancellationToken)
        {
            return await _context.Notifications.AnyAsync(x =>
                x.UserId == userId &&
                x.Type == type &&
                x.ReferenceId == referenceId &&
                !x.IsArchived,
                cancellationToken);
        }
    }
}
