using FluentValidation;
namespace Finora.Application.Features.Incomes.Commands.DeleteIncome
{
    public class DeleteIncomeCommandValidator : AbstractValidator<DeleteIncomeCommand>
    {
        public DeleteIncomeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
