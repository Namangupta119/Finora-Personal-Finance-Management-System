using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using MediatR;

namespace Finora.Application.Features.Incomes.Commands.CreateIncome
{
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, Guid>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICurrentUserService _currentUserServic;

        public CreateIncomeCommandHandler(IIncomeRepository incomeRepository, ICurrentUserService currentUserServic)
        {
            _incomeRepository = incomeRepository;
            _currentUserServic = currentUserServic;
        }

        public async Task<Guid> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = new Income
            {
                Title = request.Title,
                Description = request.Description,
                Amount = request.Amount,
                IncomeDate = request.IncomeDate,
                UserId = _currentUserServic.UserId
            };

            await _incomeRepository.AddAsync(income);

            await _incomeRepository.SaveChangesAsync();

            return income.Id;
        }
    }
}
