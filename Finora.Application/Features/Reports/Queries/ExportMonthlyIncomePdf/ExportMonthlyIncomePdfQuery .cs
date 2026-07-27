using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyIncomePdf
{
    public class ExportMonthlyIncomePdfQuery : IRequest<byte[]>
    {
        public int Year { get; set; }
    }
}
