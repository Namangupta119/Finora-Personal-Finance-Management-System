using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyIncomeReport
{
    public class ExportMonthlyIncomeReportQueryHandler : IRequestHandler<ExportMonthlyIncomeReportQuery, byte[]>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IExcelExportService _excelExportService;
        private const string WorksheetName = "Monthly Income";

        public ExportMonthlyIncomeReportQueryHandler(IIncomeRepository incomeRepository,ICurrentUserService currentUserService,IExcelExportService excelExportService)
        {
            _incomeRepository = incomeRepository;
            _currentUserService = currentUserService;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(ExportMonthlyIncomeReportQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var report = await _incomeRepository.GetMonthlyIncomeReportAsync(
                userId,
                request.Year,
                cancellationToken);

            foreach (var item in report)
            {
                item.MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(item.Month);
            }

            return _excelExportService.ExportToExcel(report,"Monthly Income",$"Monthly Income Report - {request.Year}");
        }
    }
}
