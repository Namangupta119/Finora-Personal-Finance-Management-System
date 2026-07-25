using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport
{
    public class GetCategoryWiseExpenseReportQuery : IRequest<List<CategoryWiseExpenseReportDto>>
    {
        public int Year { get; set; }
    }
}
