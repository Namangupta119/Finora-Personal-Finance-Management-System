using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Incomes.Queries.GetAllIncomes
{
    public class GetAllIncomesQueryHandler : IRequestHandler<GetAllIncomesQuery, IReadOnlyList<IncomeDto>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllIncomesQueryHandler(IIncomeRepository incomeRepository, ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<IncomeDto>> Handle(GetAllIncomesQuery request, CancellationToken cancellationToken)
        {
            var incomes = await _incomeRepository.GetIncomesAsync(_currentUserService.UserId);

            return incomes.Select(x => new IncomeDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Amount = x.Amount,
                IncomeDate = x.IncomeDate,
            }).ToList();
        }
    }
}
