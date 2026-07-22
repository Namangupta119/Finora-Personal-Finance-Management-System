using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Queries.GetGoalById
{
    public class GetGoalByIdQuery : IRequest<GetGoalByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
