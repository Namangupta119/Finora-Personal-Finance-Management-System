using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Queries.GetInvestments
{
    public class GetInvestmentsQuery : IRequest<PagedInvestmentResponse>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
