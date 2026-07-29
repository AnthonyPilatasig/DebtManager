using System;
using System.Collections.Generic;
using MediatR;

namespace DebtManager.Application.Features.Notifications.Queries.GetUnreadNotifications;

public record NotificationDto(Guid Id, string Message, DateTime CreatedAt);

public record GetUnreadNotificationsQuery(Guid UserId) : IRequest<List<NotificationDto>>;
