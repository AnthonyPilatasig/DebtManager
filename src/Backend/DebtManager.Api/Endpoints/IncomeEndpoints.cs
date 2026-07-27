using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DebtManager.Api.Endpoints;

public static class IncomeEndpoints
{
    public static void MapIncomeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/incomes").WithTags("Incomes");

        group
            .MapPost(
                "/",
                async (
                    IMediator mediator,
                    [Microsoft.AspNetCore.Mvc.FromBody]
                        DebtManager.Application.Features.Incomes.Commands.AddIncome.AddIncomeCommand command
                ) =>
                {
                    var id = await mediator.Send(command);
                    return TypedResults.Created($"/api/incomes/{id}", new { Id = id });
                }
            )
            .WithName("AddIncome")
            .WithSummary("Registra un nuevo ingreso")
            .WithDescription("Agrega una fuente de ingreso (fijo o variable) al portafolio.");

        group
            .MapGet(
                "/user/{userId:guid}",
                async (Guid userId, IMediator mediator) =>
                {
                    var incomes = await mediator.Send(
                        new DebtManager.Application.Features.Incomes.Queries.GetIncomesByUser.GetIncomesByUserQuery(
                            userId
                        )
                    );
                    return Results.Ok(incomes);
                }
            )
            .WithName("GetIncomesByUser")
            .WithSummary("Listado de ingresos")
            .WithDescription("Obtiene la lista completa de ingresos del usuario.");

        group
            .MapDelete(
                "/{id:guid}",
                async (Guid id, IMediator mediator) =>
                {
                    var result = await mediator.Send(
                        new DebtManager.Application.Features.Incomes.Commands.DeleteIncome.DeleteIncomeCommand(
                            id
                        )
                    );
                    if (!result)
                        return Results.NotFound();

                    return Results.NoContent();
                }
            )
            .WithName("DeleteIncome")
            .WithSummary("Elimina un ingreso (Soft Delete)");
    }
}
