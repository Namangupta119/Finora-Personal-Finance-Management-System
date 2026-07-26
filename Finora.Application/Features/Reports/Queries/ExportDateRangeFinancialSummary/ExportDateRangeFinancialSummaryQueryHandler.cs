using Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummary
{
    public class ExportDateRangeFinancialSummaryQueryHandler : IRequestHandler<ExportDateRangeFinancialSummaryQuery, byte[]>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IExcelExportService _excelExportService;

        private const string WorksheetName = "Financial Summary";

        public ExportDateRangeFinancialSummaryQueryHandler(IIncomeRepository incomeRepository,IExpenseRepository expenseRepository,ICurrentUserService currentUserService,IExcelExportService excelExportService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _currentUserService = currentUserService;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(ExportDateRangeFinancialSummaryQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var totalIncome = await _incomeRepository.GetTotalIncomeByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            var totalExpense = await _expenseRepository.GetTotalExpenseByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            var incomeTransactionCount = await _incomeRepository.GetIncomeTransactionCountByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            var expenseTransactionCount = await _expenseRepository.GetExpenseTransactionCountByDateRangeAsync(
                userId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            var report = new DateRangeFinancialSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetSavings = totalIncome - totalExpense,
                TransactionCount = incomeTransactionCount + expenseTransactionCount
            };

            return _excelExportService.ExportToExcel(
                new List<DateRangeFinancialSummaryDto> { report },
                WorksheetName,
                $"Financial Summary ({request.StartDate:yyyy-MM-dd} to {request.EndDate:yyyy-MM-dd})");
        }
    }

}
