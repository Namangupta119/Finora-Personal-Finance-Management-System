using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace Finora.Application.Notifications.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand>
    {
        private readonly INotificationRepository _notificationRepositor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepositor, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _notificationRepositor = notificationRepositor;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepositor.GetByIdAsync(request.NotificationId, _currentUserService.UserId, cancellationToken);

            if (notification is null)
                throw new NotFoundException("Notification not found.");

            if (notification.IsRead)
                return;

            notification.IsRead = true;
            notification.ReadOn = DateTimeOffset.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
