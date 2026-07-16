using Finora.Domain.Common;
using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities.Identity
{
    public class UserSetting : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public Theme Theme { get; set; }
        public string DateFormat { get; set; } = "dd/MM/yyyy";
        public string NumberFormat { get; set; } = "en-IN";
        public User User { get; set; } = null!;
    }
}
