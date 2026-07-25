using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
    {
        public MarkNotificationAsReadCommandValidator()
        {
            RuleFor(x => x.NotificationId).NotEmpty();
        }
    }
}
