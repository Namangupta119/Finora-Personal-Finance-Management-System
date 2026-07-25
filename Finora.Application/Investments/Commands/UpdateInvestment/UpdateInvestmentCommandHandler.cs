using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Commands.UpdateInvestment
{
    public class UpdateInvestmentCommandHandler : IRequestHandler<UpdateInvestmentCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateInvestmentCommandHandler(ICurrentUserService currentUserService, IInvestmentRepository investmentRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _investmentRepository = investmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateInvestmentCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var investment = await _investmentRepository.GetByIdAsync(
                request.InvestmentId,
                userId,
                cancellationToken);

            if (investment is null)
            {
                throw new NotFoundException("Investment not found.");
            }

            investment.Name = request.Name;
            investment.Symbol = request.Symbol;
            investment.Type = request.Type;
            investment.Quantity = request.Quantity;
            investment.PurchasePrice = request.PurchasePrice;
            investment.CurrentPrice = request.CurrentPrice;
            investment.PurchaseDate = request.PurchaseDate;
            investment.Broker = request.Broker;
            investment.Notes = request.Notes;

            await _investmentRepository.UpdateAsync(investment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
