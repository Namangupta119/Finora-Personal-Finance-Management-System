using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.Queries.GetNetWorthDashboard
{
    public class GetNetWorthDashboardValidator : AbstractValidator<GetNetWorthDashboardQuery>
    {
        public GetNetWorthDashboardValidator()
        {
        }
    }
}
