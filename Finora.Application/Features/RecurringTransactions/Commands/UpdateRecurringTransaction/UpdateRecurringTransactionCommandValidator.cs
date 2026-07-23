using FluentValidation;

namespace Finora.Application.Features.RecurringTransactions.Commands.UpdateRecurringTransaction
{
    public class UpdateRecurringTransactionCommandValidator : AbstractValidator<UpdateRecurringTransactionCommand>
    {
        public UpdateRecurringTransactionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category Id is required.");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.").MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.TransactionType).IsInEnum();

            RuleFor(x => x.Frequency).IsInEnum();

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start Date is required.");

            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).When(x => x.EndDate.HasValue).WithMessage("End Date must be greater than Start Date");
        }
    }
}
