using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.ExportCategoryWiseExpenseReport
{
    public class ExportCategoryWiseExpenseReportQuery : IRequest<byte[]>
    {
        public int Year { get; set; }
    }
}
