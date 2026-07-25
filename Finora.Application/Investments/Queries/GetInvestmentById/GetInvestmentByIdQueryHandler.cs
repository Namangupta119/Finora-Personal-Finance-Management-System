using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Queries.GetInvestmentById
{
    public class GetInvestmentByIdQueryHandler : IRequestHandler<GetInvestmentByIdQuery, InvestmentDetailsDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInvestmentRepository _investmentRepository;
        public GetInvestmentByIdQueryHandler(ICurrentUserService currentUserService, IInvestmentRepository investmentRepository)
        {
            _currentUserService = currentUserService;
            _investmentRepository = investmentRepository;
        }

        public async Task<InvestmentDetailsDto> Handle(GetInvestmentByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var investment = await _investmentRepository.GetByIdAsync(request.InvestmentId,userId,cancellationToken);

            if (investment is null)
            {
                throw new NotFoundException("Investment not found.");
            }

            return new InvestmentDetailsDto
            {
                Id = investment.Id,
                Name = investment.Name,
                Symbol = investment.Symbol,
                Type = investment.Type,
                Quantity = investment.Quantity,
                PurchasePrice = investment.PurchasePrice,
                CurrentPrice = investment.CurrentPrice,
                PurchaseDate = investment.PurchaseDate,
                Broker = investment.Broker,
                Notes = investment.Notes
            };
        }
    }
}
