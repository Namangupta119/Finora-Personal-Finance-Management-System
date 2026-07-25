using Finora.Application.Features.RecurringTransactions.Commands.DeleteRecurringTransaction;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotificationCommandHandler(INotificationRepository notificationRepository,ICurrentUserService currentUserService,IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId, _currentUserService.UserId, cancellationToken);

            if (notification is null)
                throw new DirectoryNotFoundException("Notification not found.");

            await _notificationRepository.DeleteAsync(notification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
