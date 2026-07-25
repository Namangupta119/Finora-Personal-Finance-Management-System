using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetInvestmentReport
{
    public class GetInvestmentReportQueryHandler : IRequestHandler<GetInvestmentReportQuery, List<InvestmentReportDto>>
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetInvestmentReportQueryHandler(IInvestmentRepository investmentRepository,ICurrentUserService currentUserService)
        {
            _investmentRepository = investmentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<InvestmentReportDto>> Handle(GetInvestmentReportQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var investments = await _investmentRepository.GetAllByUserIdAsync(
                userId,
                cancellationToken);

            var report = investments.Select(x =>
            {
                var totalInvestedAmount = x.Quantity * x.PurchasePrice;
                var currentValue = x.Quantity * x.CurrentPrice;
                var profitLoss = currentValue - totalInvestedAmount;

                var profitLossPercentage = totalInvestedAmount == 0 ? 0 : (profitLoss / totalInvestedAmount) * 100;

                return new InvestmentReportDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Quantity = x.Quantity,
                    PurchasePrice = x.PurchasePrice,
                    CurrentPrice = x.CurrentPrice,
                    TotalInvestedAmount = Math.Round(totalInvestedAmount, 2, MidpointRounding.AwayFromZero),
                    CurrentValue = Math.Round(currentValue, 2, MidpointRounding.AwayFromZero),
                    ProfitLoss = Math.Round(profitLoss, 2, MidpointRounding.AwayFromZero),
                    ProfitLossPercentage = Math.Round(profitLossPercentage, 2, MidpointRounding.AwayFromZero),
                    PurchaseDate = x.PurchaseDate,
                    Broker = x.Broker,
                    Symbol = x.Symbol
                };
            }).ToList();

            return report;
        }
    }
}
