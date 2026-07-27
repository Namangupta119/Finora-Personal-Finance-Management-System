using Finora.Application.Exceptions;
using Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendMonthlyExpenseReportEmail
{
    public class SendMonthlyExpenseReportEmailCommandHandler : IRequestHandler<SendMonthlyExpenseReportEmailCommand>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;

        public SendMonthlyExpenseReportEmailCommandHandler(
            IMediator mediator,
            IPdfExportService pdfExportService,
            IEmailService emailService,
            ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
            _emailService = emailService;
            _currentUserService = currentUserService;
        }
        public async Task Handle(SendMonthlyExpenseReportEmailCommand request,CancellationToken cancellationToken)
        {

            const string reportTitle = "Monthly Expense Report";

            var subject = $"Finora - {reportTitle}";

            var fileName = $"MonthlyExpenseReport-{request.Year}.pdf";

            var body = $"""
            <h2>{reportTitle}</h2>
            <p>Please find your {request.Year} monthly expense report attached.</p>
            <p>Regards,<br/>Finora Team</p>
            """;

            // Step 1
            var report = await _mediator.Send(new GetMonthlyExpenseReportQuery
            {
                Year = request.Year,
            }, cancellationToken);

            if (!report.Any())
            {
                throw new NotFoundException(
                    "No monthly expense report found for the selected year.");
            }

            // Step 2
            var pdf = _pdfExportService.ExportToPdf(
                report,
                reportTitle);

            // Step 3
            await _emailService.SendEmailAsync(
                _currentUserService.Email,
                subject,
                body,
                pdf,
                fileName);
        }
    }
}
