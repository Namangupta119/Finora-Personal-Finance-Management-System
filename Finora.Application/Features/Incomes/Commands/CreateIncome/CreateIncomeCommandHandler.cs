using Finora.Application.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public CreateIncomeCommandHandler(IIncomeRepository incomeRepository, ICurrentUserService currentUserServic, IUnitOfWork unitOfWork)
        {
            _incomeRepository = incomeRepository;
            _currentUserServic = currentUserServic;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.SaveChangesAsync();

            return income.Id;
        }
    }
}
