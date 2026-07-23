using Finora.Domain.Enums;
using MediatR;
namespace Finora.Application.Features.RecurringTransactions.Commands.UpdateRecurringTransaction
{
    public class UpdateRecurringTransactionCommand : IRequest<UpdateRecurringTransactionResponse>
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public RecurrenceFrequency Frequency { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }
    }
}
