using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyExpenseReport
{
    public class ExportMonthlyExpenseReportQueryHandler : IRequestHandler<ExportMonthlyExpenseReportQuery, byte[]>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IExcelExportService _excelExportService;

        public ExportMonthlyExpenseReportQueryHandler(IExpenseRepository expenseRepository,ICurrentUserService currentUserService,IExcelExportService excelExportService)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(ExportMonthlyExpenseReportQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var report = await _expenseRepository.GetMonthlyExpenseReportAsync(
                userId,
                request.Year,
                cancellationToken);

            foreach (var item in report)
            {
                item.MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(item.Month);
            }

            return _excelExportService.ExportToExcel(report,"Monthly Expense",$"Monthly Expense Report - {request.Year}");
        }
    }
}
