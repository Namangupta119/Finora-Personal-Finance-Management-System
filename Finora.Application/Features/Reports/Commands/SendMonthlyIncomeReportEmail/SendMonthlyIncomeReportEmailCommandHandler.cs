using Finora.Application.Exceptions;
using Finora.Application.Features.Reports.Queries.GetMonthlyIncomeReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendMonthlyIncomeReportEmail
{
    public class SendMonthlyIncomeReportEmailCommandHandler : IRequestHandler<SendMonthlyIncomeReportEmailCommand>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;

        public SendMonthlyIncomeReportEmailCommandHandler(IMediator mediator, IPdfExportService pdfExportService, IEmailService emailService, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
            _emailService = emailService;
            _currentUserService = currentUserService;
        }
        public async Task Handle(SendMonthlyIncomeReportEmailCommand request, CancellationToken cancellationToken)
        {
            const string reportTitle = "Monthly Income Report";

            var subject = $"Finora - {reportTitle}";

            var fileName = $"MonthlyIncomeReport-{request.Year}.pdf";

            var report = await _mediator.Send(
                new GetMonthlyIncomeReportQuery
                {
                    Year = request.Year
                },
                cancellationToken);

            if (!report.Any())
            {
                throw new NotFoundException(
                    "No monthly income report found for the selected year.");
            }

            var pdf = _pdfExportService.ExportToPdf(
                report,
                reportTitle);

            var body = $"""
                <h2>{reportTitle}</h2>

                <p>Please find your {request.Year} monthly income report attached.</p>

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
