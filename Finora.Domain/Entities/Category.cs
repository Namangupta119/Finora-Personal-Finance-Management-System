using Finora.Domain.Common;
using Finora.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string IconKey { get; set; } = default!;
        public string ColorKey { get; set; } = default!;
        public int DisplayOrder { get; set; }
        public bool IsSystem { get; set; }
        public bool IsArchived { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }

    }
}
