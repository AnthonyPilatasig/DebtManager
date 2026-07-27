using DebtManager.Application.Features.Users.Queries.GetOrCreateUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DebtManager.Api.Endpoints;

public static class UserEndpoints
{
    public record AuthRequest(string Email);

    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/users").WithTags("Users");

        group
            .MapPost(
                "/register",
                async (IMediator mediator, [FromBody] AuthRequest request) =>
                {
                    if (string.IsNullOrWhiteSpace(request.Email))
                        return Results.BadRequest("El correo electrónico es requerido.");

                    try
                    {
                        var query =
                            new DebtManager.Application.Features.Users.Commands.RegisterUser.RegisterUserCommand(
                                request.Email
                            );
                        var userId = await mediator.Send(query);
                        return Results.Ok(new { UserId = userId });
                    }
                    catch (InvalidOperationException ex)
                    {
                        return Results.Conflict(ex.Message);
                    }
                }
            )
            .WithName("RegisterUser")
            .WithSummary("Registra un nuevo usuario formalmente (Preparado para Cloud Sync)");

        group
            .MapPost(
                "/login",
                async (IMediator mediator, [FromBody] AuthRequest request) =>
                {
                    if (string.IsNullOrWhiteSpace(request.Email))
                        return Results.BadRequest("El correo electrónico es requerido.");

                    try
                    {
                        var query =
                            new DebtManager.Application.Features.Users.Queries.Login.LoginUserQuery(
                                request.Email
                            );
                        var userId = await mediator.Send(query);
                        return Results.Ok(new { UserId = userId });
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        return Results.Unauthorized();
                    }
                }
            )
            .WithName("LoginUser")
            .WithSummary("Inicia sesión de un usuario existente");

        group
            .MapDelete(
                "/{id:guid}",
                async (Guid id, IMediator mediator) =>
                {
                    var command =
                        new DebtManager.Application.Features.Users.Commands.DeleteUser.DeleteUserCommand(
                            id
                        );
                    var success = await mediator.Send(command);

                    if (!success)
                        return Results.NotFound();
                    return Results.NoContent();
                }
            )
            .WithName("DeleteUser")
            .WithSummary("Elimina permanentemente al usuario y todos sus datos (GDPR/LOPDP)");
    }
}
