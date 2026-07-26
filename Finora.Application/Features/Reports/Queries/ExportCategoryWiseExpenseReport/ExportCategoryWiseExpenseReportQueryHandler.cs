using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpenseReport
{
    public class ExportCategoryWiseExpenseReportQueryHandler : IRequestHandler<ExportCategoryWiseExpenseReportQuery, byte[]>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IExcelExportService _excelExportService;

        private const string WorksheetName = "Category Wise Expense";

        public ExportCategoryWiseExpenseReportQueryHandler(IExpenseRepository expenseRepository,ICurrentUserService currentUserService,IExcelExportService excelExportService)
        {
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(ExportCategoryWiseExpenseReportQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var report = await _expenseRepository.GetCategoryWiseExpenseReportAsync(userId,request.Year,cancellationToken);

            return _excelExportService.ExportToExcel(report,WorksheetName,$"Category Wise Expense Report - {request.Year}");
        }
    }
}
