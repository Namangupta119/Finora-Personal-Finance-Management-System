using Finora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities.Identity
{
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; set;  }
        public string Token { get; set;  } = string.Empty;
        public DateTimeOffset ExpiresOn { get; set;  }
        public bool IsRevoked { get; set; }
        public DateTimeOffset? RevokedOn { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}
