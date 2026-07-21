using Finora.Domain.Common;
using Finora.Domain.Entities.Identity;

namespace Finora.Domain.Entities
{
    public class Income : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset IncomeDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public bool IsArchived { get; set; }
    }
}
