using Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportDateRangeFinancialSummaryPdf
{
    public class ExportDateRangeFinancialSummaryPdfQueryHandler : IRequestHandler<ExportDateRangeFinancialSummaryPdfQuery, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;

        public ExportDateRangeFinancialSummaryPdfQueryHandler(IMediator mediator,IPdfExportService pdfExportService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(ExportDateRangeFinancialSummaryPdfQuery request,CancellationToken cancellationToken)
        {
            var report = await _mediator.Send(
                new GetDateRangeFinancialSummaryQuery
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                },
                cancellationToken);

            return _pdfExportService.ExportToPdf(
                new List<DateRangeFinancialSummaryDto> { report },
                $"Financial Summary Report ({request.StartDate:dd MMM yyyy} - {request.EndDate:dd MMM yyyy})");
        }
    }
}
