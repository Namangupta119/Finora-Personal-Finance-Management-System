using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportInvestmentPdf
{
    public class ExportInvestmentPdfQuery : IRequest<byte[]>
    {
    }
}
