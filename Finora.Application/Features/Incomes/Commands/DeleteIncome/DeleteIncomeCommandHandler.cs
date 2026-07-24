using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Incomes.Commands.DeleteIncome
{
    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIncomeCommandHandler(IIncomeRepository incomeRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _incomeRepository = incomeRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var income = await _incomeRepository.GetByIdAsync(request.Id, userId);

            if (income == null)
                throw new NotFoundException("Income not found.");

            _incomeRepository.Remove(income);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
