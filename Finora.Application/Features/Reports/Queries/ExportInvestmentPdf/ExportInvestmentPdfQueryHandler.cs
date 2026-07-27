using Finora.Application.Features.Reports.Queries.GetInvestmentReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportInvestmentPdf
{
    public class ExportInvestmentPdfQueryHandler : IRequestHandler<ExportInvestmentPdfQuery, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;

        public ExportInvestmentPdfQueryHandler(IMediator mediator,IPdfExportService pdfExportService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(ExportInvestmentPdfQuery request,CancellationToken cancellationToken)
        {
            var report = await _mediator.Send(new GetInvestmentReportQuery(),cancellationToken);

            return _pdfExportService.ExportToPdf(
                report,
                "Investment Report",
                true);
        }
    }
}
