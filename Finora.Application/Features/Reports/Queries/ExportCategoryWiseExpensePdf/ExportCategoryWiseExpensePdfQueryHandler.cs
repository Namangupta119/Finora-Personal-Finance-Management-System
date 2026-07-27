using Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpensePdf
{
    public class ExportCategoryWiseExpensePdfQueryHandler : IRequestHandler<ExportCategoryWiseExpensePdfQuery, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;

        public ExportCategoryWiseExpensePdfQueryHandler(IMediator mediator,IPdfExportService pdfExportService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(ExportCategoryWiseExpensePdfQuery request,CancellationToken cancellationToken)
        {
            var report = await _mediator.Send(
                new GetCategoryWiseExpenseReportQuery
                {
                    Year = request.Year
                },
                cancellationToken);

            return _pdfExportService.ExportToPdf(
                report,
                $"Category Wise Expense Report - {request.Year}");
        }
    }
}
