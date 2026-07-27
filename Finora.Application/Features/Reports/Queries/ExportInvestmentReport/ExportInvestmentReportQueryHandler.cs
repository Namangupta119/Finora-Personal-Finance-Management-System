using Finora.Application.Features.Reports.Queries.GetInvestmentReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportInvestmentReport
{
    public class ExportInvestmentReportQueryHandler : IRequestHandler<ExportInvestmentReportQuery, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IExcelExportService _excelExportService;

        private const string WorksheetName = "Investments";

        public ExportInvestmentReportQueryHandler(IMediator mediator,IExcelExportService excelExportService)
        {
            _mediator = mediator;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(ExportInvestmentReportQuery request,CancellationToken cancellationToken)
        {
            var report = await _mediator.Send(new GetInvestmentReportQuery(),cancellationToken);

            return _excelExportService.ExportToExcel(
                report,
                WorksheetName,
                "Investment Report");
        }
    }
}
