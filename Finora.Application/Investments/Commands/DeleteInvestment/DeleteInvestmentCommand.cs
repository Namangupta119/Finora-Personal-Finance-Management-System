using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Commands.DeleteInvestment
{
    public class DeleteInvestmentCommand : IRequest
    {
        public Guid InvestmentId { get; set; }
    }
}
