using MediatR;

namespace Finora.Application.Features.Budget.Commands.CreateBudget
{
    public class CreateBudgetCommand : IRequest<Guid>
    {
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
