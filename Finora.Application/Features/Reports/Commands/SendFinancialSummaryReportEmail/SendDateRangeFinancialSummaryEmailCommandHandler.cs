using Finora.Application.Features.Reports.Queries.GetDateRangeFinancialSummary;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendFinancialSummaryReportEmail
{
    public class SendDateRangeFinancialSummaryEmailCommandHandler : IRequestHandler<SendDateRangeFinancialSummaryEmailCommand>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;

        public SendDateRangeFinancialSummaryEmailCommandHandler(IMediator mediator, IPdfExportService pdfExportService, IEmailService emailService, ICurrentUserService currentUserService)
        { 
            _mediator = mediator;
            _pdfExportService = pdfExportService;
            _emailService = emailService;
            _currentUserService = currentUserService;
        }
        public async Task Handle(SendDateRangeFinancialSummaryEmailCommand request, CancellationToken cancellationToken)
        {
            const string reportTitle = "Financial Summary Report";

            var subject = $"Finora - {reportTitle}";

            var fileName = $"FinancialSummaryReport-{request.StartDate:yyyyMMdd}-{request.EndDate:yyyyMMdd}.pdf";

            var report = await _mediator.Send(
                new GetDateRangeFinancialSummaryQuery
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                },
                cancellationToken);

            // Single DTO → Array
            var pdf = _pdfExportService.ExportToPdf(
                new[] { report },
                reportTitle);

            var body = $"""
            <h2>{reportTitle}</h2>

            <p>Please find your financial summary report for the selected date range attached.</p>

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
