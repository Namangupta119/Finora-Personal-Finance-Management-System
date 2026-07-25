using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Investments.Queries.GetInvestmentDashboard
{
    public class GetInvestmentDashboardQueryHandler : IRequestHandler<GetInvestmentDashboardQuery, InvestmentDashboardDto>
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetInvestmentDashboardQueryHandler(IInvestmentRepository investmentRepository,ICurrentUserService currentUserService)
        {
            _investmentRepository = investmentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<InvestmentDashboardDto> Handle(GetInvestmentDashboardQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var investments = await _investmentRepository.GetAllByUserIdAsync(
                userId,
                cancellationToken);

            var totalInvestedAmount = investments.Sum(x =>
                x.Quantity * x.PurchasePrice);

            var currentPortfolioValue = investments.Sum(x =>
                x.Quantity * x.CurrentPrice);

            var profitLoss =
                currentPortfolioValue - totalInvestedAmount;

            var profitLossPercentage =
                totalInvestedAmount == 0 ? 0 : (profitLoss / totalInvestedAmount) * 100;

            return new InvestmentDashboardDto
            {
                TotalInvestedAmount = totalInvestedAmount,
                CurrentPortfolioValue = currentPortfolioValue,
                ProfitLoss = profitLoss,
                ProfitLossPercentage = Math.Round(profitLossPercentage, 2)
            };
        }
    }
}
