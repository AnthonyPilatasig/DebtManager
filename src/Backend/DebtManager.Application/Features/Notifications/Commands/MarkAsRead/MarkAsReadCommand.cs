using System;
using MediatR;

namespace DebtManager.Application.Features.Notifications.Commands.MarkAsRead;

public record MarkAsReadCommand(Guid NotificationId) : IRequest;
