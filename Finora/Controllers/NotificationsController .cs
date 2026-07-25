using Finora.Application.Notifications.Commands.CreateTestNotification;
using Finora.Application.Notifications.Commands.DeleteNotification;
using Finora.Application.Notifications.Commands.MarkAllNotificationAsRead;
using Finora.Application.Notifications.Commands.MarkNotificationAsRead;
using Finora.Application.Notifications.Queries.GetNotifications;
using Finora.Application.Notifications.Queries.GetUnreadNotificationCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Finora.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadNotificationCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUnreadNotificationCountQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpPut("{notificationId:guid}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(Guid notificationId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new MarkNotificationAsReadCommand
            {
                NotificationId = notificationId
            }, cancellationToken);

            return NoContent();
        }

        [HttpPut("mark-all-as-read")]
        public async Task<IActionResult> MarkAllAsRead(CancellationToken cancellationToken)
        {
            await _mediator.Send(new MarkAllNotificationsAsReadCommand(),cancellationToken);

            return NoContent();
        }

        [HttpDelete("{notificationId:guid}")]
        public async Task<IActionResult> Delete(Guid notificationId,CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteNotificationCommand
                {
                    NotificationId = notificationId
                },
                cancellationToken);

            return NoContent();
        }

        [HttpPost("test")]
        public async Task<IActionResult> CreateTestNotification(CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateTestNotificationCommand(), cancellationToken);

            return Ok();
        }
    }
}
