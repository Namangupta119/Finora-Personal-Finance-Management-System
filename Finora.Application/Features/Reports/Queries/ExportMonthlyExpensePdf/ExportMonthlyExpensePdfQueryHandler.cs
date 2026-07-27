using Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyExpensePdf
{
    public class ExportMonthlyExpensePdfQueryHandler : IRequestHandler<ExportMonthlyExpensePdfQuery, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;

        public ExportMonthlyExpensePdfQueryHandler(IMediator mediator,IPdfExportService pdfExportService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(ExportMonthlyExpensePdfQuery request,CancellationToken cancellationToken)
        {
            var report = await _mediator.Send(
                new GetMonthlyExpenseReportQuery
                {
                    Year = request.Year
                },
                cancellationToken);

            return _pdfExportService.ExportToPdf(report,$"Monthly Expense Report - {request.Year}");
        }
    }
}
