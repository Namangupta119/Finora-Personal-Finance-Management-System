using Finora.Application.Exceptions;
using Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendCategoryExpenseReportEmail
{
    public class SendCategoryExpenseReportEmailCommandHandler : IRequestHandler<SendCategoryExpenseReportEmailCommand>
    {
        private readonly IMediator _mediator;
        private readonly IPdfExportService _pdfExportService;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;

        public SendCategoryExpenseReportEmailCommandHandler(IMediator mediator, IPdfExportService pdfExportService, IEmailService emailService, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _pdfExportService = pdfExportService;
            _emailService = emailService;
            _currentUserService = currentUserService;
        }
        public async Task Handle(SendCategoryExpenseReportEmailCommand request, CancellationToken cancellationToken)
        {
            const string reportTitle = "Category-wise Expense Report";

            var subject = $"Finora - {reportTitle}";

            var fileName = $"CategoryExpenseReport-{request.Year}.pdf";

            var report = await _mediator.Send(
            new GetCategoryWiseExpenseReportQuery
            {
                Year = request.Year
            },
            cancellationToken);

            if (!report.Any())
            {
                throw new NotFoundException(
                    "No category-wise expense report found for the selected year.");
            }

            var pdf = _pdfExportService.ExportToPdf(report,reportTitle);

            var body = $"""
            <h2>{reportTitle}</h2>

            <p>Please find your <strong>{request.Year}</strong> category-wise expense report attached.</p>

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
