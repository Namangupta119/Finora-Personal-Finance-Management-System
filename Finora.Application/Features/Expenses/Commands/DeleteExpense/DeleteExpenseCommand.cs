using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Expenses.Commands.DeleteExpense
{
    public record DeleteExpenseCommand(Guid Id) : IRequest;
}
