using Finora.Application.Common.Models;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Application.Notifications.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, PagedResult<NotificationDto>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetNotificationsQueryHandler(INotificationRepository notificationRepository, ICurrentUserService currentUserService)
        {
            _notificationRepository = notificationRepository;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var notifications = await _notificationRepository.GetByUserIdAsync(userId, request.PageNumber, request.PageSize, cancellationToken);

            var totalRecords = await _notificationRepository.GetTotalCountAsync(userId, cancellationToken);

            var items = notifications.Select(x => new NotificationDto
            {
                Id = x.Id,
                Title = x.Title,
                Message = x.Message,
                Type = x.Type,
                IsRead = x.IsRead,
                CreatedOn = x.CreatedOn,
                ReadOn = x.ReadOn,
                ActionUrl = x.ActionUrl
            }).ToList();

            return new PagedResult<NotificationDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords
            };
        }
    }
}
