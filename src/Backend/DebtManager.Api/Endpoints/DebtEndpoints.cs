using DebtManager.Application.Features.Debts.Commands.AddDebt;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace DebtManager.Api.Endpoints;

public record CreateDebtResponse(Guid Id);

public static class DebtEndpoints
{
    public static void MapDebtEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/debts").WithTags("Debts");

        group.MapPost("/", async (AddDebtCommand command, IMediator mediator) =>
        {
            var debtId = await mediator.Send(command);
            return TypedResults.Created($"/api/debts/{debtId}", new CreateDebtResponse(debtId));
        })
        .WithName("AddDebt")
        .WithSummary("Registra una nueva deuda")
        .WithDescription("Permite agregar una deuda al portafolio del usuario indicando el tipo, saldo, cuota e interés.")
        .Produces<CreateDebtResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/user/{userId:guid}", async (Guid userId, IMediator mediator) =>
        {
            var debts = await mediator.Send(new DebtManager.Application.Features.Debts.Queries.GetDebtsByUserId.GetDebtsByUserIdQuery(userId));
            return TypedResults.Ok(debts);
        })
        .WithName("GetDebtsByUser")
        .WithSummary("Obtiene las deudas activas de un usuario")
        .WithDescription("Devuelve una lista completa de deudas vigentes asociadas a un ID de usuario.")
        .Produces<List<DebtManager.Domain.Entities.Debt>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("/{id:guid}", async (Guid id, DebtManager.Application.Features.Debts.Commands.UpdateDebt.UpdateDebtCommand command, IMediator mediator) =>
        {
            if (id != command.Id) return Results.BadRequest();
            var success = await mediator.Send(command);
            return success ? TypedResults.NoContent() : Results.NotFound();
        })
        .WithName("UpdateDebt")
        .WithSummary("Actualiza una deuda")
        .WithDescription("Permite modificar los detalles de una deuda existente como el saldo, nombre o cuota.")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var success = await mediator.Send(new DebtManager.Application.Features.Debts.Commands.DeleteDebt.DeleteDebtCommand(id));
            return success ? TypedResults.NoContent() : Results.NotFound();
        })
        .WithName("DeleteDebt")
        .WithSummary("Elimina una deuda (Soft Delete)")
        .WithDescription("Marca la deuda como eliminada sin borrarla físicamente de la base de datos.")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
