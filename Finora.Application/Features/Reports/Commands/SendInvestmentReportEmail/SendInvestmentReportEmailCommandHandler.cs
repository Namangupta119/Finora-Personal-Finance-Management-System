using Finora.Application.Exceptions;
using Finora.Application.Features.Reports.Queries.GetInvestmentReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendInvestmentReportEmail
{
    public class SendInvestmentReportEmailCommandHandler : IRequestHandler<SendInvestmentReportEmailCommand>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;

        public SendInvestmentReportEmailCommandHandler(IMediator mediator, IPdfExportService pdfExportService, IEmailService emailService, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
            _emailService = emailService;
            _currentUserService = currentUserService;
        }
        public async Task Handle(SendInvestmentReportEmailCommand request, CancellationToken cancellationToken)
        {
            const string reportTitle = "Investment Report";

            var subject = $"Finora - {reportTitle}";

            var fileName = $"InvestmentReport-{DateTime.UtcNow:yyyyMMdd}.pdf";

            var report = await _mediator.Send(new GetInvestmentReportQuery(),cancellationToken);

            if (!report.Any())
            {
                throw new NotFoundException(
                    "No investment report found.");
            }

            var pdf = _pdfExportService.ExportToPdf(report,reportTitle,isLandscape: true);

            var body = """
            <h2>Investment Report</h2>

            <p>Please find your investment report attached.</p>

            <p>Regards,<br/>Finora Team</p>
            """;

            await _emailService.SendEmailAsync(
            _currentUserService.Email,
            subject,
            body,
            pdf,
            fileName);
            }
        }
}
