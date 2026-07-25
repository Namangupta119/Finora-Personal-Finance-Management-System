using Finora.Domain.Common;
using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public NotificationType Type { get; set; }

        public bool IsRead { get; set; }

        public DateTimeOffset? ReadOn { get; set; }

        public string? ActionUrl { get; set; }
        public bool IsArchived { get; set; }
        public Guid? ReferenceId { get; set; }

        public string? ReferenceType { get; set; }
    }
}
