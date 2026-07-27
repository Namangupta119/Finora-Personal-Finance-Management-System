using Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Reports.Commands.SendCategoryExpenseReportEmail
{
    public record SendCategoryExpenseReportEmailCommand(int Year) : IRequest;
}
