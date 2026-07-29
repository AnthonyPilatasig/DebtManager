using System;
using System.Collections.Generic;
using DebtManager.Application.Features.Notifications.Commands.MarkAsRead;
using DebtManager.Application.Features.Notifications.Queries.GetUnreadNotifications;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DebtManager.Api.Endpoints;

public static class NotificationEndpoints
{
    public static void MapNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/notifications").WithTags("Notifications");

        group
            .MapGet(
                "/user/{userId:guid}/unread",
                async (Guid userId, IMediator mediator) =>
                {
                    var notifications = await mediator.Send(
                        new GetUnreadNotificationsQuery(userId)
                    );
                    return TypedResults.Ok(notifications);
                }
            )
            .WithName("GetUnreadNotifications")
            .Produces<List<NotificationDto>>(StatusCodes.Status200OK);

        group
            .MapPut(
                "/{id:guid}/read",
                async (Guid id, IMediator mediator) =>
                {
                    await mediator.Send(new MarkAsReadCommand(id));
                    return TypedResults.NoContent();
                }
            )
            .WithName("MarkAsRead")
            .Produces(StatusCodes.Status204NoContent);
    }
}
