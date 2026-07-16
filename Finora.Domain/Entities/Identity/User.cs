using Finora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash{ get; set; }= string.Empty;
        public bool EmailConfirmed { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset? LastLoginOn { get; set; }
        public DateTimeOffset? LastPasswordChangedOn { get; set; }
        public Profile? Profile { get; set; }
        public UserSetting? UserSetting { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
