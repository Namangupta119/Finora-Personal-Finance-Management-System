using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport
{
    public class GetMonthlyIncomeReportQueryHandler : IRequestHandler<GetMonthlyIncomeReportQuery, List<MonthlyIncomeReportDto>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMonthlyIncomeReportQueryHandler(IIncomeRepository incomeRepository,ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<MonthlyIncomeReportDto>> Handle(GetMonthlyIncomeReportQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var report = await _incomeRepository.GetMonthlyIncomeReportAsync(userId,request.Year,cancellationToken);

            foreach (var item in report)
            {
                item.MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(item.Month);
            }

            return report;
        }
    }
}
