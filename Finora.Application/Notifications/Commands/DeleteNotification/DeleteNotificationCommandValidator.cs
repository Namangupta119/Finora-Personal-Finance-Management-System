using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty();
        }
    }
}
