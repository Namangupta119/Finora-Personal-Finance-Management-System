using Finora.Domain.Common;
using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities.Identity
{
    public class Profile : BaseEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTimeOffset? DateOfBirth {  get; set; }
        public Gender Gender { get; set; }
        public string Address {  get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}
