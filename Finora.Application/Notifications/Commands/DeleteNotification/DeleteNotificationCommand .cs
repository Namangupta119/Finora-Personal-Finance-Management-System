using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommand : IRequest
    {
        public Guid NotificationId { get; set; }
    }
}
