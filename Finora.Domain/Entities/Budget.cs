using Finora.Domain.Common;
using Finora.Domain.Entities.Identity;

namespace Finora.Domain.Entities
{
    public class Budget : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public User User { get; set; } = default!;
        public Category Category { get; set; } = default!;
    }
}
