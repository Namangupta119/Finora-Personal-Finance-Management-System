using Finora.Application.Exceptions;
using Finora.Application.Features.Incomes.Queries.GetAllIncomes;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Incomes.Queries.GetIncomeById
{
    public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, IncomeDto>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetIncomeByIdQueryHandler(IIncomeRepository incomeRepository, ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IncomeDto> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var income = await _incomeRepository.GetByIdAsync(request.Id, userId);

            if (income == null)
                throw new NotFoundException("Income not found.");

            return new IncomeDto
            {
                Id = income.Id,
                Title = income.Title,
                Description = income.Description,
                Amount = income.Amount,
                IncomeDate = income.IncomeDate
            };
        }
    }
}
