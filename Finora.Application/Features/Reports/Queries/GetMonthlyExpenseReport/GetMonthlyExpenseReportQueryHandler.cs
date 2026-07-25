using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport
{
    public class GetMonthlyExpenseReportQueryHandler : IRequestHandler<GetMonthlyExpenseReportQuery, List<MonthlyExpenseReportDto>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMonthlyExpenseReportQueryHandler(IExpenseRepository expenseRepository,ICurrentUserService currentUserService)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<MonthlyExpenseReportDto>> Handle(GetMonthlyExpenseReportQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var report = await _expenseRepository.GetMonthlyExpenseReportAsync(
                userId,
                request.Year,
                cancellationToken);

            foreach (var item in report)
            {
                item.MonthName = CultureInfo.CurrentCulture
                    .DateTimeFormat
                    .GetMonthName(item.Month);
            }

            return report;
        }
    }
}
