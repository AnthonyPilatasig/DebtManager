using System;
using DebtManager.Application.Features.CashFlow.Queries.CalculateFreeCashFlow;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DebtManager.Api.Endpoints;

public record FreeCashFlowResponse(Guid UserId, decimal FreeCashFlow);

public static class CashFlowEndpoints
{
    public static void MapCashFlowEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cashflow").WithTags("Cash Flow");

        group
            .MapGet(
                "/{userId:guid}",
                async (Guid userId, IMediator mediator) =>
                {
                    var result = await mediator.Send(new CalculateFreeCashFlowQuery(userId));
                    return TypedResults.Ok(new FreeCashFlowResponse(userId, result));
                }
            )
            .WithName("GetFreeCashFlow")
            .WithSummary("Calcula el Flujo de Caja Libre (Ingresos Fijos - Deudas)")
            .WithDescription(
                "Obtiene la sumatoria de todos los ingresos fijos y le resta las cuotas mínimas mensuales de todas las deudas activas."
            )
            .Produces<FreeCashFlowResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group
            .MapGet(
                "/{userId:guid}/projections",
                async (Guid userId, IMediator mediator) =>
                {
                    var result = await mediator.Send(
                        new DebtManager.Application.Features.CashFlow.Queries.GetProjections.GetProjectionsQuery(
                            userId
                        )
                    );
                    return TypedResults.Ok(result);
                }
            )
            .WithName("GetProjections")
            .WithSummary("Obtiene planes algorítmicos de desendeudamiento")
            .WithDescription(
                "Aplica las fórmulas de Bola de Nieve y Avalancha a las deudas del usuario."
            );
    }
}
