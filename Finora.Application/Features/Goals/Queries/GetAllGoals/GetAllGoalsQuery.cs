using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Goals.Queries.GetAllGoals
{
    public class GetAllGoalsQuery : IRequest<List<GetAllGoalsResponse>>
    {
    }
}
