using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
namespace Finora.Application.Features.Incomes.Commands.UpdateIncome
{
    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIncomeRepository _incomeRepository;

        public UpdateIncomeCommandHandler(ICurrentUserService currentUserService, IIncomeRepository incomeRepository)
        {
            _currentUserService = currentUserService;
            _incomeRepository = incomeRepository;
        }

        public async Task Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var income = await _incomeRepository.GetByIdAsync(request.Id, userId);

            if (income == null)
                throw new NotFoundException("Income not found.");

            income.Title = request.Title;
            income.Description = request.Description;
            income.Amount = request.Amount;
            income.IncomeDate = request.IncomeDate;

            _incomeRepository.Update(income);

            await _incomeRepository.SaveChangesAsync();
        }
    }
}
