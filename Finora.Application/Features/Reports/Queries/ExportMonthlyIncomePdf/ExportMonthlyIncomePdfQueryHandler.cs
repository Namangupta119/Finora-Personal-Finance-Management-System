using Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport;
using Finora.Application.Interfaces.Services;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyIncomePdf
{
    public class ExportMonthlyIncomePdfQueryHandler : IRequestHandler<ExportMonthlyIncomePdfQuery, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;

        public ExportMonthlyIncomePdfQueryHandler(IMediator mediator,IPdfExportService pdfExportService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(ExportMonthlyIncomePdfQuery request,CancellationToken cancellationToken)
        {
            var report = await _mediator.Send(
                new GetMonthlyIncomeReportQuery
                {
                    Year = request.Year
                },
                cancellationToken);

            return _pdfExportService.ExportToPdf(
                report,
                $"Monthly Income Report - {request.Year}");
        }
    }

}
