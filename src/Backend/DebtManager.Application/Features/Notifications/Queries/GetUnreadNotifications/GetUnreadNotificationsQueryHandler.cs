using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Notifications.Queries.GetUnreadNotifications;

public class GetUnreadNotificationsQueryHandler : IRequestHandler<GetUnreadNotificationsQuery, List<NotificationDto>>
{
    private readonly IAppDbContext _context;

    public GetUnreadNotificationsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationDto>> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await _context.Notifications
            .Where(n => n.UserId == request.UserId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NotificationDto(n.Id, n.Message, n.CreatedAt))
            .ToListAsync(cancellationToken);

        return notifications;
    }
}
