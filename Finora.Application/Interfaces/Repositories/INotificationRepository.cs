using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);

        Task<Notification?> GetByIdAsync(Guid notificationId,Guid userId, CancellationToken cancellationToken);

        Task<List<Notification>> GetByUserIdAsync(Guid userId,int pageNumber,int pageSize,CancellationToken cancellationToken);
        Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken);

        Task MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken);

        Task DeleteAsync(Notification notification);
        Task<int> GetTotalCountAsync(Guid userId, CancellationToken cancellationToken);
    }
}
