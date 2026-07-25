using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public NotificationType Type { get; set; }

        public bool IsRead { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? ReadOn { get; set; }

        public string? ActionUrl { get; set; }
    }
}
