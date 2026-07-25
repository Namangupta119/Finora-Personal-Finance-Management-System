using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport
{
    public class GetCategoryWiseExpenseReportQueryHandler : IRequestHandler<GetCategoryWiseExpenseReportQuery, List<CategoryWiseExpenseReportDto>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCategoryWiseExpenseReportQueryHandler(IExpenseRepository expenseRepository, ICurrentUserService currentUserService)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<CategoryWiseExpenseReportDto>> Handle(GetCategoryWiseExpenseReportQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            return await _expenseRepository.GetCategoryWiseExpenseReportAsync(_currentUserService.UserId,request.Year,cancellationToken);
        }
    }
}
