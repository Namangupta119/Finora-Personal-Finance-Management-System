using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Investments.Queries.GetInvestments
{
    public class GetInvestmentsQueryHandler : IRequestHandler<GetInvestmentsQuery, PagedInvestmentResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInvestmentRepository _investmentRepository;

        public GetInvestmentsQueryHandler(ICurrentUserService currentUserService, IInvestmentRepository investmentRepository)
        {
            _currentUserService = currentUserService;
            _investmentRepository = investmentRepository;
        }

        public async Task<PagedInvestmentResponse> Handle(GetInvestmentsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var investments = await _investmentRepository.GetByUserIdAsync(userId,request.PageNumber,request.PageSize,cancellationToken);

            var totalCount = await _investmentRepository.GetTotalCountAsync(userId,cancellationToken);

            var response = new PagedInvestmentResponse
            {
                Investments = investments.Select(x => new InvestmentDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Symbol = x.Symbol,
                    Type = x.Type,
                    Quantity = x.Quantity,
                    PurchasePrice = x.PurchasePrice,
                    CurrentPrice = x.CurrentPrice,
                    PurchaseDate = x.PurchaseDate,
                    Broker = x.Broker
                }).ToList(),

                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return response;
        }
    }
}
