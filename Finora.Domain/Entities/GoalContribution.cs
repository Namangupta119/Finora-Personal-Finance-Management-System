using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Domain.Entities
{
    public class GoalContribution
    {
        public Guid Id { get; set; }

        public Guid GoalId { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset ContributionDate { get; set; }

        public string? Notes { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }

        public bool IsArchived { get; set; }

        // Navigation Property
        public Goal Goal{ get; set; } = default!;
    }
}
