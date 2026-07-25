using Finora.Application.Features.RecurringTransactions.Commands.CreateRecurringTransaction;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using Finora.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Notifications.Commands.CreateTestNotification
{
    public class CreateTestNotificationCommandHandler : IRequestHandler<CreateTestNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTestNotificationCommandHandler(
         INotificationRepository notificationRepository,
         ICurrentUserService currentUserService,
         IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CreateTestNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                UserId = _currentUserService.UserId,
                Title = "Test Notification",
                Message = "This is a test notification.",
                Type = NotificationType.General,
                IsRead = false,
                CreatedOn = DateTimeOffset.UtcNow,
                ActionUrl = "/dashboard"
            };

            await _notificationRepository.AddAsync(notification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
