using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset? DeletedOn { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
