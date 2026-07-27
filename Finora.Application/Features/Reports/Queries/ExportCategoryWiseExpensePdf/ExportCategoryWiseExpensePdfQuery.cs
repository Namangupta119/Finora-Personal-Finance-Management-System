using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpensePdf
{
    public class ExportCategoryWiseExpensePdfQuery : IRequest<byte[]>
    {
        public int Year { get; set; }
    }
}
