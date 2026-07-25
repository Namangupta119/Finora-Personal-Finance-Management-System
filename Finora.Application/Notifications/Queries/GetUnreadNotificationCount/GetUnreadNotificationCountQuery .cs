using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Queries.GetUnreadNotificationCount
{
    public class GetUnreadNotificationCountQuery : IRequest<int>
    {
    }
}
