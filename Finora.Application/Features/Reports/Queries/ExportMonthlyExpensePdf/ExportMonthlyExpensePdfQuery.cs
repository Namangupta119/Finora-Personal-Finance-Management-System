using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportMonthlyExpensePdf
{
    public class ExportMonthlyExpensePdfQuery : IRequest<byte[]>
    {
        public int Year { get; set; }
    }
}
