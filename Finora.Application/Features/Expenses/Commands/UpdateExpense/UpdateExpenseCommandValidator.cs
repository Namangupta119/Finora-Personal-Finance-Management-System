using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Commands.UpdateExpense
{
    public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
    {
        public UpdateExpenseCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Description).MaximumLength(500);

            RuleFor(x => x.Amount).GreaterThan(0);

            RuleFor(x => x.CategoryId).NotEmpty();

            RuleFor(x => x.ExpenseDate).LessThanOrEqualTo(DateTimeOffset.Now).WithMessage("Expense date cannot be in the future.");
        }
    }
}
