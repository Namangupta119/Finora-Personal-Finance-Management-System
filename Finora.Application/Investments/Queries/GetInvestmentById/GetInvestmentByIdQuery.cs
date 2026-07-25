using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Queries.GetInvestmentById
{
    public class GetInvestmentByIdQuery : IRequest<InvestmentDetailsDto>
    {
        public Guid InvestmentId { get; set; }
    }
}
