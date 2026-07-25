using Finora.Application.Common.Models;
using Finora.Application.Notifications.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQuery : IRequest<PagedResult<NotificationDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
