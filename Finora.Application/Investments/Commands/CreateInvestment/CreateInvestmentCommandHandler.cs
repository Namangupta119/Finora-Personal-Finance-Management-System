using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Commands.CreateInvestment
{
    public class CreateInvestmentCommandHandler : IRequestHandler<CreateInvestmentCommand, Guid>
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInvestmentCommandHandler(
            IInvestmentRepository investmentRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _investmentRepository = investmentRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
            CreateInvestmentCommand request,
            CancellationToken cancellationToken)
        {
            var investment = new Investment
            {
                UserId = _currentUserService.UserId,
                Name = request.Name,
                Symbol = request.Symbol,
                Type = request.Type,
                Quantity = request.Quantity,
                PurchasePrice = request.PurchasePrice,
                CurrentPrice = request.CurrentPrice,
                PurchaseDate = request.PurchaseDate,
                Broker = request.Broker,
                Notes = request.Notes,
                IsArchived = false
            };

            await _investmentRepository.AddAsync(investment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return investment.Id;
        }
    }
}
