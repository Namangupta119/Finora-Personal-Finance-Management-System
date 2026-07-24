using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System.Runtime.InteropServices.Marshalling;
namespace Finora.Application.Features.Incomes.Commands.UpdateIncome
{
    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateIncomeCommandHandler(ICurrentUserService currentUserService, IIncomeRepository incomeRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _incomeRepository = incomeRepository;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
